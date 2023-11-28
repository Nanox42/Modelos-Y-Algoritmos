using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    public int defaultSpeed;
    public int Speed;
    public int jump;
    public int defaultJump;
    public float rotateSpeed;
    public float gravityScale;
    public Vector3 movement;
    public CharacterController controller;
    public GameObject characterModel;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Speed = defaultSpeed;
        jump = defaultJump;
    }

    // Update is called once per frame
    void Update()
    {
        movement = movement.normalized * Speed;
        movement.y = movement.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(movement * Time.deltaTime);

    }
}
