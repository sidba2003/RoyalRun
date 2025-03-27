using TMPro;
using UnityEngine;

public class Coin : Collectible
{
    ScoreScript script;
    TimeManager timeManager;


    private void Start()
    {
        timeManager = TimeManager.Instance;
        script = ScoreScript.instance;
    }

    protected override void onPickup()
    {
        if (!timeManager.getGameOver())
        {
            // increasing the score
            script.updateText();
            Destroy(gameObject);
        }
    }
}
