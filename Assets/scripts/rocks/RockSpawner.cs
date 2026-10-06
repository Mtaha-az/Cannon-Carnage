using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject[] rockPrefabs;
    public Transform[] spawnPoints;
    public float spawnTime = 5f;
    private int currentRockIndex = 0;
    private WinCondition winCondition; // Reference to WinCondition script

    void Start()
    {
        winCondition = FindObjectOfType<WinCondition>(); // Find WinCondition in the scene
        SpawnRock();
        StartCoroutine(SpawnRocks());
    }

    IEnumerator SpawnRocks()
    {
        while (currentRockIndex < rockPrefabs.Length)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnRock();
        }

        // Notify WinCondition when all rocks are spawned
        if (winCondition != null && currentRockIndex >= rockPrefabs.Length)
        {
            winCondition.OnSpawningComplete(currentRockIndex);
        }
    }

    void SpawnRock()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(rockPrefabs[currentRockIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
        currentRockIndex++;
    }

    public void RespawnRock(GameObject rock)
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        rock.transform.position = spawnPoints[randomSpawnIndex].position;

        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
