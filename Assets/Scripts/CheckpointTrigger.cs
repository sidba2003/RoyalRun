using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] float timeIncrease;

    TimeManager timeManager;
    public static CheckpointTrigger instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeManager = TimeManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           if (!timeManager.getGameOver()) timeManager.increaseTimer(timeIncrease);
        }
    }

    public float getTimerIncreaseValue()
    {
        return timeIncrease;
    }
}
