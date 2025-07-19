using System.Threading.Tasks;
using BepInEx;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace GorillaInfo
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class GorillaInfoPlugin : BaseUnityPlugin
    {
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

        private void Start() => GorillaTagger.OnPlayerSpawned(async delegate
        {
            if (infoTextObject == null)
            {
                infoTextObject = new GameObject("GorillaInfoText");
                infoText = infoTextObject.AddComponent<TextMeshPro>();

                Transform cam = Camera.main.transform;
                infoTextObject.transform.position = cam.position + cam.forward * 1.0f - cam.up * 0.4f;
                infoTextObject.transform.SetParent(cam);
                infoTextObject.layer = 19;

                while (infoText.font != GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().font)
                {
                    await Task.Delay(1000);
                    infoText.font = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText").GetComponent<TextMeshPro>().font;
                    infoText.fontSize = 0.35f;
                    infoText.alignment = TextAlignmentOptions.Center;
                    infoText.color = Color.white;
                    infoText.isOverlay = true;
                }
            }
        });

        private void Update()
        {
            infoText.text = Text();

            Transform cam = Camera.main.transform;
            infoTextObject.transform.LookAt(cam);
            infoTextObject.transform.Rotate(0, 180, 0);
        }
    }

    internal class Constants
    {
        public const string GUID = "net.cody.gorillainfo",
                            NAME = "GorillaInfo",
                            VERS = "3.0.0";
    }
}