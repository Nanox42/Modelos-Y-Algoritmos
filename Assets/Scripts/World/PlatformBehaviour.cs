using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float travelTime;
    private Rigidbody rb;
    private Vector3 currentPos;

    public CharacterController cc;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        currentPos = Vector3.Lerp(startPoint.position, endPoint.position,
            Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
        rb.MovePosition(currentPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerController>() != null)
        {
            other.transform.GetComponent<PlayerController>().platform = true;
            cc = other.GetComponent<CharacterController>();
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<PlayerController>() != null)
            cc.Move(rb.velocity * Time.deltaTime * 0.5f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<PlayerController>() != null)
        {
            other.transform.GetComponent<PlayerController>().platform = false;
        }
    }
}