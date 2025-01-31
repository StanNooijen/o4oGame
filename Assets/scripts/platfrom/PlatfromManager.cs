using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager instance;

    public GameObject platformPrefab;
    public GameObject platformFallingPrefab;
    public GameObject swingPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject smallPlatformPrefab;
    public GameObject RotatingPlatformPrefab;
    public GameObject LaunchPadPrefab;
    public Transform player;
    public float spawnRadiusX;
    public float spawnRate;
    private float lastSpawnHeight;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    private bool ballSpawned = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        lastSpawnHeight = player.position.y;
    }

    private void Update()
    {
        float playerY = player.position.y;
        float playerHeightDifference = playerY - lastSpawnHeight;
        int platformsNeeded = Mathf.FloorToInt(playerHeightDifference / spawnRate);

        for (int i = 0; i < platformsNeeded; i++)
        {
            SpawnPlatform();
            lastSpawnHeight += spawnRate;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void SpawnPlatform()
    {
        float ballSpawnLocation = Random.Range(3, 7);
        spawnedPlatforms.RemoveAll(platform => platform == null);

        Vector2 spawnPosition = new Vector2(Random.Range(-spawnRadiusX, spawnRadiusX), lastSpawnHeight + spawnRate + 2f);

        
            GameObject prefabToSpawn;
            float randomValue = Random.Range(0f, 100f);

            if (ScoreManager.instance.Score > 65 && randomValue <= 15f)
            {
                prefabToSpawn = platformFallingPrefab; // 20% chance
            }
            else if (ScoreManager.instance.Score > 115 && randomValue > 20f && randomValue <= 30f)
            {
                prefabToSpawn = swingPlatformPrefab; // 10% chance
                spawnPosition.y += 1f;
            }
            else if (ScoreManager.instance.Score > 165 && randomValue > 35f && randomValue <= 55f)
            {
                prefabToSpawn = movingPlatformPrefab; // 15% chance
                spawnPosition.x = 0;
            }
            else if (ScoreManager.instance.Score > 215 && randomValue > 60f && randomValue <= 70f)
            {
                prefabToSpawn = smallPlatformPrefab; // 10% chance
            }
            else if (ScoreManager.instance.Score > 265 && randomValue > 80f && randomValue <= 90f)
            {
                prefabToSpawn = RotatingPlatformPrefab; // 10% chance
                ballSpawned = true;
                
            }
            else if (randomValue > 99f && randomValue <= 100f)
            {
                prefabToSpawn = LaunchPadPrefab; // 1% chance
            }
            else
            {
                prefabToSpawn = platformPrefab; // Default platform
            }

            if (ballSpawned)
            {
                spawnPosition.x = ballSpawnLocation;
                GameObject newPlatform = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedPlatforms.Add(newPlatform);
                ballSpawned = false;
            }
            else
            {
                GameObject newPlatform = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedPlatforms.Add(newPlatform);
                ballSpawned = false;
            }
    }

    public void RemovePlatform(GameObject platform)
    {
        spawnedPlatforms.Remove(platform);
        Destroy(platform);
    }
}