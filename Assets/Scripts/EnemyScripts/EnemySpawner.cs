using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform tree;           // Assign your Tree object
    public float safeDistance = 10;  // Minimum distance from the tree
    public float spawnCooldown = 3f; // Time between spawns

    private float spawnTimer;

    void Start()
    {
        spawnTimer = spawnCooldown;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnCooldown;
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomSpawnPosition;
        int attempts = 0;

        do
        {
            randomSpawnPosition = transform.position + new Vector3(Random.Range(-30, 30), 1, Random.Range(-30, 30));
            attempts++;
        }

        while (Vector3.Distance(randomSpawnPosition, tree.position) < safeDistance && attempts < 50);

        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
    }
}
