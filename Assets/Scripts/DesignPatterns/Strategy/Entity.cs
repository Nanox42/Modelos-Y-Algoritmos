using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMovementStrategy
{
    void Movement(Entity entity);
}

public enum MovementType
{
    WayPoints,
    Force,

}

public class Entity:MonoBehaviour
{
    [SerializeField] private MovementType initialMovementType;
    public float _speed = 5f;
    public float _force = 5f;
    public List<Transform> waypoints;
    Vector3 startPoint;
    Vector3 endPoint;
    [SerializeField]
    private StrategyWaypoints strategyWaypoints;
    [SerializeField]
    private StrategyForce strategyForce;

    private IMovementStrategy movementStrategy;

    private void Start()
    {
        startPoint = waypoints[0].position;
        endPoint = waypoints[1].position;
        switch (initialMovementType)
        {
            case MovementType.WayPoints:
                //movementStrategy = strategyWaypoints;
                movementStrategy = new StrategyWaypoints(waypoints);
                 break;

            case MovementType.Force:
                //movementStrategy = strategyForce;
                movementStrategy = new StrategyForce(startPoint,endPoint);
                break;
        }
        StartCoroutine(SetMovementStrategy());

    }

    private void Update()
    {
        movementStrategy.Movement(this);
    }

    IEnumerator SetMovementStrategy()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);

            // Alternar entre las estrategias
            if (movementStrategy is StrategyWaypoints)
            {
                movementStrategy = new StrategyForce(startPoint, endPoint);
            }
            else if (movementStrategy is StrategyForce)
            {
                movementStrategy = new StrategyWaypoints(waypoints);
            }
        }
    }
}

