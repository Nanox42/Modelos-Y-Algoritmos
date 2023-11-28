using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log("collision detected");
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null)
        {
            PlayerController.Get().Jump();
            PlayerController.Get().SetEnemyCount();
            damageable.Damage();
            Debug.Log("Killed Enemy");
        }
    }

}
