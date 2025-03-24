using BepInEx;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace GorillaInfo
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class GorillaInfoPlugin : BaseUnityPlugin
    {
        private const string MyGUID = "com.SillyCody.GorillaInfo";
        private const string PluginName = "GorillaInfo";
        private const string VersionString = "2.0.1";

        private GameObject infoTextObject;
        private TextMeshPro infoText;

        private static string RoomInfo()
        {
            return PhotonNetwork.InRoom
                ? $"<color=green>Room: {PhotonNetwork.CurrentRoom.Name}</color> | Players: {PhotonNetwork.PlayerList.Length}/10"
                : "<color=red>Not in room</color>";
        }

        private static string PerformanceInfo()
        {
            float fps = Mathf.Ceil(1.0f / Time.unscaledDeltaTime);
            string fpsColor = fps <= 60 ? "<color=red>" : (fps >= 100 ? "<color=green>" : "<color=yellow>");

            return $"{fpsColor}FPS: {fps}</color> | Ping: {PhotonNetwork.GetPing()}";
        }

        public static string Text()
        {
            return $"{RoomInfo()}\n{PerformanceInfo()}\n";
        }

        private void Start() => GorillaTagger.OnPlayerSpawned(InitTextObjects);

        private void InitTextObjects()
        {
            if (infoTextObject == null)
            {
                infoTextObject = new GameObject("GorillaInfoText");
                infoText = infoTextObject.AddComponent<TextMeshPro>();

                infoText.font = GameObject.Find("motdtext").GetComponent<TextMeshPro>().font;
                infoText.fontSize = 0.35f;
                infoText.alignment = TextAlignmentOptions.Center;
                infoText.color = Color.white;
                infoText.isOverlay = true;

                Transform cam = Camera.main.transform;
                infoTextObject.transform.position = cam.position + cam.forward * 1.0f - cam.up * 0.4f;
                infoTextObject.transform.SetParent(cam);
                infoTextObject.layer = 19;
            }
        }

        private void Update()
        {
            infoText.text = Text();

            Transform cam = Camera.main.transform;
            infoTextObject.transform.LookAt(cam);
            infoTextObject.transform.Rotate(0, 180, 0);
        }
    }
}
