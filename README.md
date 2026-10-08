# Robo Rampage

Robo Rampage is a small 3D first-person shooter prototype built with Godot and C#. The player explores an arena, fights drone enemies, switches between a rifle and an SMG, collects ammunition, and avoids lava hazards.

The project is also a practical example of connecting C# gameplay scripts to Godot scenes, nodes, signals, physics, navigation, animation, and input actions.

## Features

- First-person movement, jumping, mouse look, and aim mode
- Two hitscan weapons: a high-damage rifle and an automatic SMG
- Per-weapon firing cooldown, recoil, muzzle-flash particles, and impact sparks
- Ammo types, on-screen ammo display, and collectible ammo pickups
- Drone enemies that navigate toward and attack the player
- Player health, a damage effect, lava hazards, and a game-over menu
- A main arena and a separate sandbox scene

## Controls

| Action | Input |
| --- | --- |
| Move | `W` `A` `S` `D` or arrow keys |
| Jump | `Space` |
| Look | Mouse |
| Aim | Right mouse button |
| Fire | Left mouse button |
| Equip rifle | `1` |
| Equip SMG | `2` |
| Cycle weapons | Mouse wheel up/down |
| Show cursor | `Escape` (`ui_cancel`) |

The rifle fires once per press; the SMG fires continuously while the fire button is held. Aiming narrows the camera field of view and reduces movement and mouse-look sensitivity.

## Getting started

### Requirements

- Godot Engine **4.7.2 with .NET support** (the project uses `Godot.NET.Sdk` 4.7.2)
- .NET 8 SDK for the desktop target

### Run in the editor

1. Clone or download this repository.
2. Open `project.godot` with the Godot .NET editor.
3. Allow Godot to import the assets and build the C# project when prompted.
4. Run the project. The configured startup scene is `levels/MainLevel.tscn`.

You can also build the solution with the .NET CLI from the repository directory:

```sh
dotnet build robo-rampage.sln
```

## Programming concepts demonstrated

### Godot nodes and reusable scenes

Gameplay entities are composed from Godot nodes and saved as scenes (`.tscn`). C# classes extend the node types they control—for example, `Player` and `Enemy` extend `CharacterBody3D`, while `Pickup` and `Lava` extend `Area3D`. The main level instances those scenes, allowing each entity to be authored and reused independently.

### C# scripts integrated with Godot

Scripts use Godot's C# API, partial classes, and lifecycle callbacks such as `_Ready`, `_Process`, `_PhysicsProcess`, `_Input`, and `_UnhandledInput`. `[Export]` properties expose gameplay values and node references in the Inspector, so scene instances can configure values such as movement speed, damage, and weapon fire mode without hard-coding every setting in the script.

### Input actions and frame-based updates

Named actions such as `move_forward`, `jump`, `fire`, and `aim` are configured in `project.godot`, keeping controls separate from gameplay logic. Frame updates handle presentation and continuous input, while physics updates handle character motion. Physics-dependent movement uses `delta` so gravity and other changes are time-based rather than tied to frame rate.

### Character movement and 3D transforms

The player converts 2D movement input into a normalized 3D direction relative to its transform, applies gravity and jumping, then moves with `MoveAndSlide`. Mouse motion rotates the player and camera pivot; vertical look is clamped to avoid over-rotation. Aim mode smoothly interpolates the camera field of view toward its aiming value.

### Combat, raycasts, and cooldowns

`HitscanWeapon` uses a `RayCast3D` to determine whether a shot hits an object and where impact particles should appear. A timer limits the firing rate, and a weapon setting distinguishes automatic fire from press-to-fire behavior. On a hit, damage is applied to an enemy; recoil and muzzle flashes provide visual feedback.

### State, health, and typed data

Player and enemy hit points are represented by C# properties. Their setters trigger gameplay consequences such as playing a damage animation, provoking an enemy, removing a defeated enemy, or opening the game-over menu. Ammo is organized by an `AmmoType` enum and stored in a `Dictionary`, giving the weapons and pickups a shared way to identify and update their ammunition.

### AI navigation and animation

Enemies use Godot's `NavigationAgent3D` to follow a target position, become provoked when the player enters their aggro range, and transition to an attack animation when close enough. The attack animation calls back into the enemy script to apply damage, demonstrating animation-driven gameplay events.

### Signals and UI

Area signals connect collision events to gameplay code: entering a pickup grants ammo, and entering lava sets health to zero. The ammo label is updated by the ammo handler, while the game-over menu pauses the scene tree and offers restart and quit actions.

### Reusable resources and visual effects

Weapons, enemies, pickups, hazards, materials, shaders, and particle effects are authored as reusable Godot resources/scenes. The custom crosshair draws its own UI elements, and the camera script interpolates its transform to smooth camera movement.

## Project layout

| Path | Contents |
| --- | --- |
| `scripts/` | C# gameplay, weapon, camera, UI, and pickup scripts |
| `levels/` | Main arena and sandbox scenes |
| `player/` | Player scene and its cameras, weapons, and UI |
| `enemy/` | Drone enemy scene |
| `weapons/` | Shared hitscan weapon scene, rifle, SMG, and impact effects |
| `pickups/` | Reusable ammo pickup scenes |
| `hazards/` | Lava hazard scene |
| `gameflow/` | Game-over menu scene |
| `assets/` | Models, textures, font, and other imported assets |
| `materials/`, `shaders/` | Shared visual materials and shaders |

## Project status and licensing

This repository contains a learning/prototype project rather than a packaged release. No license file is currently included.
