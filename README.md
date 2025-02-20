# GorillaInfo

**GorillaInfo** is a mod for Gorilla Tag that displays real-time in-game information for developers and players. This mod provides dynamic on-screen stats, including room details, player count, performance metrics (FPS and ping), and more.

## Features
- Displays room information such as current room name and player count.
- Shows performance data, including FPS and ping.
- Easy-to-read display overlay that follows the player's camera, providing essential info while in-game.
- Customizable font and text size.

## Installation
1. Install [BepInEx](https://github.com/BepInEx/BepInEx) framework.
2. Download the latest version of the GorillaInfo mod.
3. Place the mod `.dll` file into your BepInEx `plugins` folder.
4. Launch Gorilla Tag and the information will be shown in-game!

## How it works
The mod creates an overlay text that updates every frame with the following data:
- **Room Info**: Displays the room name and player count (if in a room).
- **Performance Info**: Shows FPS (frames per second) and ping to the server.
  
The text overlay is anchored to the camera, so it remains in view as the player moves around.

## Compatibility
This mod is designed for use with Gorilla Tag and requires the BepInEx framework for modding.
