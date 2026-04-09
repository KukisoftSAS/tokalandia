using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject[] chunkPrefabs;

    [Header("Chunk Settings")]
    [SerializeField] private float chunkLength = 25f;
    [SerializeField] private int maxChunkCount = 6;
    [SerializeField] private float startSpawnZ = -50f;

    [Header("Cleanup Settings")]
    [SerializeField] private float destroyZ = 15f;

    private float nextSpawnZ;
    private List<GameObject> activeChunks = new List<GameObject>();

    void Start()
    {
        nextSpawnZ = startSpawnZ;

        for (int i = 0; i < maxChunkCount; i++)
        {
            SpawnChunk();
            nextSpawnZ -= chunkLength;
            Debug.Log($"{i} Spawned chunk at Z: {nextSpawnZ}");
        }

        nextSpawnZ = startSpawnZ - (maxChunkCount * chunkLength * 0.5f);
    }

    void Update()
    {
        CheckAndRecycleChunks();
    }

    void SpawnChunk()
    {
        if (chunkPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, chunkPrefabs.Length);
        Vector3 spawnPosition = new Vector3(0f, 0f, nextSpawnZ);

        GameObject newChunk = Instantiate(chunkPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        activeChunks.Add(newChunk);
    }

    void CheckAndRecycleChunks()
    {
        if (activeChunks.Count == 0) return;

        GameObject oldestChunk = activeChunks[0];

        if (oldestChunk != null && oldestChunk.transform.position.z > destroyZ)
        {
            Destroy(oldestChunk);
            activeChunks.RemoveAt(0);

            SpawnChunk();
        }
    }
}