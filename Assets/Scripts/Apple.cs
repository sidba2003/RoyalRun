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
        levelGenerator.SetChunkMovementSpeed(3f);
        Destroy(gameObject);
    }
}
