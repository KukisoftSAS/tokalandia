using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Transform player;

    public float chunkLength = 25f;
    public int initialChunks = 6;

    public float spawnAheadDistance = 100f;
    public float destroyBehindDistance = 75f;

    private float nextSpawnZ = 25f;
    private List<GameObject> activeChunks = new List<GameObject>();

    void Start()
    {
        SpawnChunk(0); // Spawn the first chunk at the start
        for (int i = 1; i < initialChunks; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        // If player is getting close to the last spawned chunk area, spawn more ahead
        if (player.position.z - spawnAheadDistance < nextSpawnZ)
        {
            SpawnChunk();
        }

        RemoveChunksBehindPlayer();
    }

    void SpawnChunk(int randomIndex = -1)
    {
        if (chunkPrefabs.Length == 0) return;

        if (randomIndex == -1)
        {
            randomIndex = Random.Range(0, chunkPrefabs.Length);
        }
        Vector3 spawnPosition = new Vector3(0f, 0f, nextSpawnZ);

        GameObject newChunk = Instantiate(chunkPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        activeChunks.Add(newChunk);

        // Keep spawning further into negative Z
        nextSpawnZ -= chunkLength;
    }

    void RemoveChunksBehindPlayer()
    {
        for (int i = activeChunks.Count - 1; i >= 0; i--)
        {
            if (activeChunks[i] == null)
            {
                activeChunks.RemoveAt(i);
                continue;
            }

            float chunkZ = activeChunks[i].transform.position.z;

            // Behind player in this setup means chunk Z is GREATER than player Z
            if (chunkZ > player.position.z + destroyBehindDistance)
            {
                //Debug.Log("Distance is " + (player.position.z + destroyBehindDistance) + "Removing chunk at Z: " + chunkZ);
                Destroy(activeChunks[i]);
                activeChunks.RemoveAt(i);
            }
        }
    }
}