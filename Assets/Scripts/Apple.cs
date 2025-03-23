using UnityEngine;

public class Apple : Collectible
{
    LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = LevelGenerator.instance;
    }
    protected override void onPickup()
    {
        Destroy(gameObject);
        levelGenerator.SetChunkMovementSpeed(3f);
    }
}
