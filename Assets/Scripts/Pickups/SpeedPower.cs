using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPower : Pickup
{
    public int Modifier;
    public override void Activate()
    {
        Debug.Log("speed buff");
        PowerUpManager.Get().speedPowerUp(Modifier);
    }
}
