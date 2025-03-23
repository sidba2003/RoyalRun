using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public static ScoreScript instance;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void updateText()
    {
        score += 100;
        text.text = score.ToString();
    }
}
