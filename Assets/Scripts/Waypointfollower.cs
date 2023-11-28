using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypointfollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypoint = 0;
    [SerializeField] float speed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    void FixedUpdate()
    {
        if (waypoints[0] == null)
            return;

        // Verifica la distancia al waypoint actual
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 0.1f)
        {
            // Incrementa el índice del waypoint
            currentWaypoint++;

            // Si alcanzó el último waypoint, reinicia
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        // Mueve hacia el waypoint actual
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);

        // Calcula la rotación hacia el waypoint actual
        Vector3 direction = waypoints[currentWaypoint].transform.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);

            // Aplica la rotación gradualmente
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
