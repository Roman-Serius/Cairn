# Cairn

A 2D pixel-art platformer built in Unity, centered on a physics-based stone-stacking mechanic: explore a room-by-room world, then switch into a building mode where you drag, rotate and balance stones.

> **Status:** early prototype — player movement, room-based camera, pause menu and the stone-building scene are in place.

## Features

- **Platforming** — responsive movement with jump buffering and sprite flipping
- **Room-based camera** — the camera snaps (or smoothly pans) to each room as the player enters it
- **Stone building** — click and drag physics stones, rotate them while held, and let gravity settle them on release
- **Additive scenes** — the pause menu and building area load on top of the world without unloading it

## Controls

| Action | Key |
|---|---|
| Move | A / D or ← / → |
| Jump | Space |
| Open / close building mode | B |
| Drag a stone | Left mouse (hold) |
| Rotate held stone | R / E |
| Pause | Esc |

## Running it

1. Install **Unity 6000.2.7f2** (Unity 6) via Unity Hub.
2. Clone this repo and open the folder in Unity Hub (**Add → Add project from disk**).
3. Open `Assets/Scenes/StartScreen.unity` and press Play.

## Project layout

```
Assets/
├── Scripts/
│   ├── Building/   StoneDrag2D — drag/rotate physics stones
│   ├── Managers/   GameManager, SceneController, CameraController, PauseManager
│   ├── Player/     PlayerController, PlayerAnimator
│   └── World/      Room, RoomCameraTrigger
├── Scenes/         StartScreen, WorldScene, Building Area, PauseMenu
├── Prefab/         Player, Room_Template, Stone
├── Animations/     Player idle / run / jump / fall
└── Tilemap/        Tile palettes and tiles
```

## Credits

- Character, background and item art: [Pixel Adventure 1](https://pixelfrog-assets.itch.io/pixel-adventure-1) by Pixel Frog
