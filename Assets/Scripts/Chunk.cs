using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] float[] laneSpawnPoints;
    [SerializeField] GameObject fence;
    [SerializeField] GameObject Apple;
    [SerializeField] float AppleSpawnChance;
    [SerializeField] GameObject Coin;
    [SerializeField] float CoinSpawnChance;

    List<int> fenceSpawnPointAvailableIndices = new List<int>() { 0, 1, 2 };

    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = LevelGenerator.instance;

        SpawnFence();
        SpawnApple();
        SpawnCoin();
    }

    void SpawnFence()
    {
        int fenceSpawnAmount = Random.Range(0, laneSpawnPoints.Length);
        for (int i = 0; i < fenceSpawnAmount; i++)
        {
            if (fenceSpawnPointAvailableIndices.Count <= 0) break;

            int spawnIndex = GetSpawnIndex();

            Vector3 spawnLocation = new Vector3(laneSpawnPoints[spawnIndex], transform.position.y, transform.position.z);
            Instantiate(fence, spawnLocation, Quaternion.identity, transform);
        }
    }

    void SpawnApple()
    {
        if (Random.value > AppleSpawnChance) return;

        if (fenceSpawnPointAvailableIndices.Count <= 0) return;

        int spawnIndex = GetSpawnIndex();

        Vector3 spawnLocation = new Vector3(laneSpawnPoints[spawnIndex], transform.position.y, transform.position.z);
        Instantiate(Apple, spawnLocation, Quaternion.identity, transform);
    }

    void SpawnCoin()
    {
        if (Random.value > CoinSpawnChance) return;

        if (fenceSpawnPointAvailableIndices.Count <= 0) return;

        int spawnIndex = GetSpawnIndex();
        float spawnXPosition = laneSpawnPoints[spawnIndex];

        int chunkLength = levelGenerator.chunkLength;
        int coinSpawnDistance = chunkLength / 5;    // we need to generate 5 coins at max, so need the uniform distance they can be spawned at

        List<int> coinSpawnZPoints = new List<int>() { -coinSpawnDistance * 2, -coinSpawnDistance, 0, coinSpawnDistance, 2 * coinSpawnDistance };

        int coinSpawnAmount = Random.Range(1, coinSpawnZPoints.Count + 1);
        while (coinSpawnAmount > 0)
        {
            int ZSpawnArrayIndex = Random.Range(0, coinSpawnZPoints.Count);

            Vector3 spawnLocation = new Vector3(spawnXPosition, transform.position.y, transform.position.z + coinSpawnZPoints[ZSpawnArrayIndex]);
            Instantiate(Coin, spawnLocation, Quaternion.identity, transform);
            coinSpawnZPoints.RemoveAt(ZSpawnArrayIndex);

            coinSpawnAmount--;
        }
    }

    int GetSpawnIndex()
    {
        int randomIndex = Random.Range(0, fenceSpawnPointAvailableIndices.Count);
        int spawnIndex = fenceSpawnPointAvailableIndices[randomIndex];
        fenceSpawnPointAvailableIndices.RemoveAt(randomIndex);

        return spawnIndex;
    }
}
