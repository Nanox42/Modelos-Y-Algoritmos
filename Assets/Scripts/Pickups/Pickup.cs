using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public GameObject particles;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            Activate();
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public abstract void Activate();

   
}
