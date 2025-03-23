using TMPro;
using UnityEngine;

public class Coin : Collectible
{
    ScoreScript script;

    private void Start()
    {
        script = ScoreScript.instance;
    }

    protected override void onPickup()
    {
        // increasing the score
        script.updateText();
        Destroy(gameObject); 
    }
}
