using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] protected int rotationSpeed = 100;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPickup();
        }
    }

    protected abstract void onPickup();
}
