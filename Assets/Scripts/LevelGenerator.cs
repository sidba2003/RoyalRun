using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int chunksAmount;
    [SerializeField] Transform chunksParent;
    [SerializeField] public int chunkLength;
    [SerializeField] float moveSpeed;

    List<GameObject> chunks = new List<GameObject>();

    public static LevelGenerator instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    void SpawnChunks()
    {
        float spawnZPoint = 0;

        for (int i = 0; i < chunksAmount; i++)
        {
            Vector3 chunkSpawnLocation = new Vector3(transform.localPosition.x, transform.localPosition.y, spawnZPoint);
            chunks.Add(Instantiate(chunkPrefab, chunkSpawnLocation, Quaternion.identity, chunksParent));

            spawnZPoint += chunkLength;
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-Vector3.forward * Time.deltaTime * moveSpeed);

            if (chunk.transform.localPosition.z <= Camera.main.transform.localPosition.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                float lastChunkZPosition = chunks[chunks.Count - 1].transform.localPosition.z + chunkLength;

                // spawning a new chunk, after destroying one
                Vector3 spawnLocation = new Vector3(transform.localPosition.x,transform.localPosition.y, lastChunkZPosition);
                chunks.Add(Instantiate(chunkPrefab, spawnLocation, Quaternion.identity, chunksParent));
            }
        }
    }
}
