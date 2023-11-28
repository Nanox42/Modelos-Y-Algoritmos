using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmortalPower : Pickup
{
    public override void Activate()
    {
        PowerUpManager.Get().inmortalPowerUp();
    }
}
