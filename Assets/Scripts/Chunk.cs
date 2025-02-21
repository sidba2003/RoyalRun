using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] float[] laneSpawnPoints;
    [SerializeField] GameObject fence;

    private void Start()
    {
        SpawnFence();
    }

    void SpawnFence()
    {
        Vector3 spawnPoint = new Vector3(laneSpawnPoints[Random.Range(0, laneSpawnPoints.Length)], transform.position.y, transform.position.z);
        Instantiate(fence, spawnPoint, Quaternion.identity, transform);
    }
}
