using System.Collections;
using UnityEngine;

public class BladesSpawner : MonoBehaviour
{
    // Array of spawn points where prefabs will spawn
    public Transform[] spawnPoints;

    // Prefab to spawn
    public GameObject prefabToSpawn;

    // Time interval between spawns
    public float spawnInterval = 5f;

    // The maximum number of prefabs to spawn
    public int maxSpawns = 5;

    // Counter to keep track of how many prefabs have been spawned
    private int spawnCount = 0;

    // Start the spawning process
    private void Start()
    {
        // Begin spawning coroutine
        StartCoroutine(SpawnObjects());
    }

    // Coroutine to spawn prefabs at specified intervals
    IEnumerator SpawnObjects()
    {
        while (spawnCount < maxSpawns)
        {
            // Wait for the specified time before spawning
            yield return new WaitForSeconds(spawnInterval);

            // Pick a random spawn point from the array
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Spawn the prefab at the selected spawn point
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Increase the spawn count
            spawnCount++;
        }
    }
}
