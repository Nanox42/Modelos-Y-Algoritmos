using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypoint = 0;
    public MovementBase Mover;
    // Start is called before the first frame update
    void Start()
    {
        Mover = GetComponent<MovementBase>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < .1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        Mover.movement = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Mover.Speed * Time.deltaTime);
    }
}
