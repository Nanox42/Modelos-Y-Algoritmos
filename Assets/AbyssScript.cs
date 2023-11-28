using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Death by fall");
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.RespawnPlayer();
        }
    }
}
