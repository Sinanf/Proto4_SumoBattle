# Proto4_SumoBattle

A Sumo Battle game prototype developed in Unity. The objective is to survive waves of enemies by knocking them off the platform while utilizing various power-ups.

## 📋 Project Overview

- **Genre:** Arcade / Physics Action
- **Unity Version:** 2021.3.0f1
- **Render Pipeline:** Built-in Render Pipeline (Standard 3D)

## 🎮 Gameplay Mechanics

- **Movement:** Physics-based movement. The player moves in the direction the camera is facing.
- **Goal:** Knock all enemies off the platform.
- **Waves:** Enemies increase with each wave.
- **Power-ups:**
  - **Type 1 (Push):** Increases knockback force against enemies.
  - **Type 2 (Destroy):** Instantly destroys enemies on contact.
  - **Type 3 (Launch):** Launches enemies upwards.

## 📂 Project Structure

### Key Directories
- `Assets/Scripts/`: Contains all C# scripts for game logic.
- `Assets/Scenes/`: Contains the main game scene (`Prototype 4`).
- `Assets/Prefabs/`: Contains Player, Enemy, and Power-up prefabs.
- `Assets/Course Library/`: Contains base assets (models, materials).

### Key Scripts
- **`PlayerController.cs`**: Handles player physics, input, and power-up interactions.
- **`SpawnManager.cs`**: Manages enemy waves and power-up spawning logic.
- **`Enemy.cs`**: Controls enemy behavior (chasing the player).
- **`RotateCamera.cs`**: Handles camera rotation around the focal point.

## 🚀 How to Start from Scratch (Migration Guide)

If you want to move this project to a new Unity version or start fresh, follow these steps:

### Option A: Export/Import Package (Recommended)
1. Open this project in Unity 2021.3.x.
2. Right-click the `Assets` folder in the Project view.
3. Select **Export Package...**.
4. Ensure "Include dependencies" is checked and click **Export**. Save the file (e.g., `SumoBattle.unitypackage`).
5. Create a new 3D Project in your desired Unity version (e.g., Unity 6 or 2022 LTS).
6. In the new project, go to **Assets > Import Package > Custom Package**.
7. Select your exported file and click **Import**.
8. Open `Assets/Scenes/Prototype 4` to start.

### Option B: Manual Migration
1. Create a new Unity 3D Project.
2. Copy the entire `Assets` folder from this repository into the new project's folder (merging with the existing Assets folder).
3. If you have errors regarding `TextMeshPro`:
   - Go to **Window > TextMeshPro > Import TMP Essentials**.
