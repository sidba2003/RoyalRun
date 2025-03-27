using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] GameObject[] chunkPrefab;
    [SerializeField] int chunksAmount;
    [SerializeField] Transform chunksParent;
    [SerializeField] public int chunkLength;
    [SerializeField] float moveSpeed;
    [SerializeField] float minMovementSpeed = 2f;
    [SerializeField] float maxMovementSpeed = 10f;
    [SerializeField] float minGravity = -2f;
    [SerializeField] float maxGravity = -20f;
    [SerializeField] int verticalFOVSpeedIncreaseValue = 12;
    [SerializeField] int verticalFOVSpeedDecreaseValue = -8;
    [SerializeField] GameObject CheckpointPrefab;
    [SerializeField] int checkPointSpawnDistance;

    List<GameObject> chunks = new List<GameObject>();
    public static LevelGenerator instance;
    CameraController controller;
    int spawnedChunksAmount;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnedChunksAmount = 0;
        controller = CameraController.instance;
        SpawnChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    GameObject chunkToSpawn()
    {
        spawnedChunksAmount++;

        if (spawnedChunksAmount % checkPointSpawnDistance == 0)
        {
            return CheckpointPrefab;
        }
        else
        {
            int rand = Random.Range(0, chunkPrefab.Length);
            return chunkPrefab[rand];
        }
    }

    void SpawnChunks()
    {
        float spawnZPoint = 0;

        for (int i = 0; i < chunksAmount; i++)
        {
            Vector3 chunkSpawnLocation = new Vector3(transform.localPosition.x, transform.localPosition.y, spawnZPoint);
            chunks.Add(Instantiate(chunkToSpawn(), chunkSpawnLocation, Quaternion.identity, chunksParent));

            spawnZPoint += chunkLength;
        }
    }

    public void SetChunkMovementSpeed(float amount)
    {
        moveSpeed = Mathf.Min(Mathf.Max(moveSpeed + amount, minMovementSpeed), maxMovementSpeed);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Mathf.Max(Mathf.Min(Physics.gravity.z - amount, minGravity), maxGravity));

        // if the amount is negative, means we need to slow down and send the FOVDecreaseValue, else we send the increase value in the other case
        controller.SetVerticalFOV((amount <  0) ? verticalFOVSpeedDecreaseValue : verticalFOVSpeedIncreaseValue);
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
                chunks.Add(Instantiate(chunkToSpawn(), spawnLocation, Quaternion.identity, chunksParent));
            }
        }
    }
}
