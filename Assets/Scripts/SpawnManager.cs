using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public GameObject powerupPrefab;

    private float spawnRange = 9;
    public int waveNumber = 1;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
        SpawnEnemyWave(waveNumber);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPos(), powerupPrefab.transform.rotation);
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
    }

    // Generate random Vector3 for spawning
    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    // Increase enemy count +1 in every wave
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs, GenerateSpawnPos(), enemyPrefabs.transform.rotation);
        }
    }
}
