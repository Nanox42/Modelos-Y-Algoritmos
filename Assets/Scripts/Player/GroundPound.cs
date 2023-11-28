using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    public float poundForce;
    public PlayerController pc;
    public CharacterController controller;
    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if(controller.isGrounded == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                pc.movement.y -= poundForce;
            }
        }

    }
}
