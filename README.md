# Proto4_SumoBattle - Development Guide

This documentation provides a step-by-step guide to recreating the **Sumo Battle** game from scratch in the latest version of Unity (2022 LTS, Unity 6, etc.).

## 🛠️ Prerequisites

1.  **Install Unity**: Download Unity Hub and install the latest LTS version.
2.  **Create Project**: Open Unity Hub > New Project > **3D Core**. Name it "SumoBattle".

---

## 🏗️ Step 1: Scene Setup

### The Arena
1.  Right-click in Hierarchy > **3D Object > Cylinder**. Name it "Island".
    -   Scale: `(10, 0.5, 10)` (Flatten it).
    -   Position: `(0, -0.25, 0)`.
2.  Create a **Material** (Assets > Create > Material) named "GroundMat". Set its color to something distinct and drag it onto the Island.

### The Camera System
This game uses a "Focal Point" system where the camera rotates around the center, and the player moves relative to the camera's view.

1.  Create an Empty Object (Right-click > Create Empty). Name it **"Focal Point"**.
    -   Position: `(0, 0, 0)`.
2.  Drag the **Main Camera** onto the "Focal Point" to make it a child.
    -   Set Camera Position: `(0, 5, -10)` approx.
    -   Set Camera Rotation: `(20, 0, 0)` to look down.
3.  Create script `RotateCamera.cs` and attach it to **Focal Point**.

**`RotateCamera.cs` Logic:**
```csharp
using UnityEngine;
public class RotateCamera : MonoBehaviour {
    public float rotationSpeed = 50.0f;
    void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
```

---

## 🏃 Step 2: The Player

1.  Create a **Sphere**. Name it "Player". Position `(0, 0, 0)`.
2.  Add Component > **Rigidbody**.
    -   Mass: `1`.
    -   Drag: `1` (helps control).
3.  Create script `PlayerController.cs` and attach to Player.

**`PlayerController.cs` Logic:**
- Needs reference to `Focal Point` to know "forward".
- Needs `Rigidbody` to apply force.
```csharp
using UnityEngine;
public class PlayerController : MonoBehaviour {
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5.0f;

    void Start() {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update() {
        float forwardInput = Input.GetAxis("Vertical");
        // Move in the direction the Focal Point (Camera) is facing
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
}
```

---

## 🤖 Step 3: The Enemy

1.  Create a **Sphere**. Name it "Enemy".
2.  Add Component > **Rigidbody**.
3.  Create script `Enemy.cs` and attach it.
4.  **Important**: Drag this Enemy into your `Assets/Prefabs` folder to make it a **Prefab**, then delete it from the scene.

**`Enemy.cs` Logic:**
- Finds the "Player" and moves towards it.
- Destroys itself if it falls off the map (`y < -10`).
```csharp
using UnityEngine;
public class Enemy : MonoBehaviour {
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    void Start() {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update() {
        if(player == null) return; // Safety check
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10) Destroy(gameObject);
    }
}
```

---

## 🌊 Step 4: Spawn Manager (Waves)

1.  Create an Empty Object. Name it **"SpawnManager"**.
2.  Create script `SpawnManager.cs` and attach it.
3.  Assign your **Enemy Prefab** to the script slot in the Inspector.

**`SpawnManager.cs` Logic:**
- Spawns enemies at random positions.
- Checks if enemy count is 0, then starts next wave.
```csharp
using UnityEngine;
public class SpawnManager : MonoBehaviour {
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int waveNumber = 1;

    void Start() {
        SpawnEnemyWave(waveNumber);
    }

    void Update() {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0) {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn) {
        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos() {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
```

---

## ⚡ Step 5: Powerups (Optional Polish)

1.  Create a small **Cube** or Diamond shape. Tag it as "Powerup".
2.  Make it a trigger: Check **Is Trigger** on the Box Collider.
3.  Update `PlayerController.cs` to detect `OnTriggerEnter` with "Powerup", set a boolean flag `hasPowerup = true`.
4.  Update `OnCollisionEnter` in `PlayerController` to apply extra force to Enemies if `hasPowerup` is true.

```csharp
// Inside PlayerController
private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Powerup")) {
        hasPowerup = true;
        Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownRoutine());
    }
}

IEnumerator PowerupCountdownRoutine() {
    yield return new WaitForSeconds(7);
    hasPowerup = false;
}

private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
        enemyRb.AddForce(awayFromPlayer * 15.0f, ForceMode.Impulse);
    }
}
```

---

## 🎮 Final Polish

- **Physics Materials**: Create a Physics Material with high Bounciness (1.0) and drag it onto the Player and Enemy colliders for a "bouncy" sumo feel.
- **Tags**: Ensure the Enemy prefab has the tag **"Enemy"**.
