using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPower : Pickup
{
    public float Modifier;
    public override void Activate()
    {
        PowerUpManager.Get().jumpPowerUp(Modifier);
    }

   
}
