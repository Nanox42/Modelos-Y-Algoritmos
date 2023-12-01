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
    [SerializeField]
    private StrategyWaypoints strategyWaypoints;
    [SerializeField]
    private StrategyForce strategyForce;

    private IMovementStrategy movementStrategy;

    private void Start()
    {
        switch (initialMovementType)
        {
            case MovementType.WayPoints:
                movementStrategy = strategyWaypoints;
                 break;

            case MovementType.Force:
                movementStrategy = strategyForce;
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
                movementStrategy = strategyWaypoints;
            }
            else if (movementStrategy is StrategyForce)
            {
                movementStrategy = strategyForce;
            }
        }
    }
}

