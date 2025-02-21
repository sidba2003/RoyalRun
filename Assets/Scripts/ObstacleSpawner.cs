using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform obstacleParent;
    [SerializeField] GameObject[] obstacles;
    [SerializeField] int objectsSpawnDelay;

    float[] obstacleSpawnPoints = { -2.5f, 0, 2.5f };
    List<GameObject> objectsSpawn = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ObjectsSpawnCoroutine());
    }

    IEnumerator ObjectsSpawnCoroutine()
    {
        while (true)
        {
            Vector3 spawnPoint = new Vector3(obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Length)], transform.position.y, transform.position.z);
            objectsSpawn.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPoint, Random.rotation, obstacleParent));

            if (objectsSpawn[0].transform.position.z <= Camera.main.transform.position.z - 5){
                Destroy(objectsSpawn[0]);
                objectsSpawn.Remove(objectsSpawn[0]);
            }

            yield return new WaitForSeconds(objectsSpawnDelay);
        }
    }
}
