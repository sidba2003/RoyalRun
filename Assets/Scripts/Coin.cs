using UnityEngine;

public class Coin : Collectible
{
    protected override void onPickup()
    {
        Destroy(gameObject);
    }
}
