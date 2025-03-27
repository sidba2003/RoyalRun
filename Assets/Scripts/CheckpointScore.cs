using TMPro;
using UnityEngine;

public class CheckpointScore : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    CheckpointTrigger trigger;

    private void Start()
    {
        trigger = CheckpointTrigger.instance;
        text.text = "+" + trigger.getTimerIncreaseValue();
    }
}
