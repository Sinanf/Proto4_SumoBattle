# Sumo Battle

A 3D sumo-style arena game built with Unity. Push enemy balls off the platform before they push you — survive as many waves as possible by collecting power-ups that give you special abilities.

## Gameplay

- Move your ball around the arena using the arrow keys / `W` `S`
- Rotate the camera with `A` `D`
- Enemies constantly home in on you and try to knock you off
- Each wave the number of enemies increases by one
- Collect power-ups to gain temporary abilities
- Survive as many waves as you can

## Power-ups

| Power-up | Effect | Duration |
|----------|--------|----------|
| **Force Burst** | Knocks enemies away with impulse force on contact | 7 seconds |
| **Destroyer** | Instantly destroys any enemy on contact | 7 seconds |
| **Sky Launch** | Launches enemies upward into the air on contact | 7 seconds |

## Controls

| Input | Action |
|-------|--------|
| `W` / `↑` | Move forward |
| `S` / `↓` | Move backward |
| `A` / `←` | Rotate camera left |
| `D` / `→` | Rotate camera right |
| `Escape` | Pause / Resume |

## Scripts

| Script | Responsibility |
|--------|---------------|
| `PlayerController.cs` | Player movement, power-up pickup & collision effects |
| `Enemy.cs` | Enemy AI — continuously moves toward the player, self-destructs when falling off |
| `SpawnManager.cs` | Wave system — spawns enemies and power-ups, tracks wave count |
| `RotateCamera.cs` | Orbits the camera around the player on horizontal input |
| `PauseManager.cs` | Pause menu, resume, and scene restart |

## Built With

- **Unity** (2021 LTS)
- **C#**
- **TextMesh Pro** — wave counter UI
- **Unity Physics** — Rigidbody-based movement and impulse forces

## Notes

This project was built as part of the [Unity Junior Programmer Pathway](https://learn.unity.com/pathway/junior-programmer) (Prototype 4). It covers Rigidbody physics, coroutines, wave-based spawning, and scene management.
