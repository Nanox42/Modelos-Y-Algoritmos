using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Pickup
{
    public int value;
    public override void Activate()
    {
        GameManager.Get().AddCoins(value);
        AudioManager.Get().CoinSFX();

    }

}
