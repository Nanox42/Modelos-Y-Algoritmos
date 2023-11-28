using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody platformRigidbody;

    public StickToPlatform stickToPlatform;
    private Vector3 currentPos;

    private void Start()
    {
        platformRigidbody = stickToPlatform.rb;
    }

   

    private void OnTriggerEnter(Collider other)
    {
       characterController = other.GetComponent<CharacterController>();
    }

    private void OnTriggerStay(Collider other)
    {
        characterController.Move(platformRigidbody.velocity * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
   