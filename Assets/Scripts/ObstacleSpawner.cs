using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform obstacleParent;
    [SerializeField] GameObject obstacle;
    [SerializeField] int objectsSpawnAmount;
    [SerializeField] int objectsSpawnDelay;

    private void Start()
    {
        StartCoroutine(ObjectsSpawnCoroutine());
    }

    IEnumerator ObjectsSpawnCoroutine()
    {
        for (int i = 0; i < objectsSpawnAmount; i++)
        {
            Instantiate(obstacle, transform.position, Quaternion.identity, obstacleParent);
            yield return new WaitForSeconds(objectsSpawnDelay);
        }
    }
}
