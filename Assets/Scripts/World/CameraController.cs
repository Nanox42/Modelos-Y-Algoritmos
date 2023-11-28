using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    public Transform pivot;

    [Header("Variables")]
    [SerializeField] private bool UseOffset;
    [SerializeField] private float sensitivity=5f;
    [SerializeField] private float maxViewAngle=45f;
    [SerializeField] private float minViewAngle=-45f;
    [SerializeField] private bool invertY;
    void Start()
    {
        target = PlayerController.Get().transform;

        if (!UseOffset)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PlayerController.Get() == null)
            return;

        pivot.transform.position = target.transform.position;
        //detecting X axis mouse movement
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;
        pivot.Rotate(0, horizontal, 0);

        //detecting Y axis mouse movement
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;

        if(invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //applying mouse movement to camera
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(target);
    }
}
