using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public int maxEnemies = 5;
    public float spawnInterval = 6f;
    public float minXRange = -10f; 
    public float maxXRange = 10f;
    public float minYRange = -10f;
    public float maxYRange = 10f;

    private int currentEnemyCount = 0;
    private GameObject[] spawnedEnemies;

    void Start()
    {
        spawnedEnemies = new GameObject[maxEnemies];
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(minXRange, maxXRange), UnityEngine.Random.Range(minYRange, maxYRange));

            spawnedEnemies[currentEnemyCount] = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);

            currentEnemyCount++;

            if (currentEnemyCount < maxEnemies)
            {
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void DestroyExistingEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
    }
}
