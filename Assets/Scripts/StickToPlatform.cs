using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float platformSpeed=3f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    public Transform target;

    public bool isActive = true;
    public Rigidbody rb;

    private void Awake()
    {
        target = pointB;
        rb = platform.GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, target.position, platformSpeed * Time.deltaTime);
        rb.MovePosition(platform.transform.position);

        if (Vector3.Distance(platform.transform.position, target.position) < 0.01f)
        {
            if (target == pointB)
            {
                target = pointA;
            }
            else
            {
                target = pointB;
            }     
        }  
    }    
}
