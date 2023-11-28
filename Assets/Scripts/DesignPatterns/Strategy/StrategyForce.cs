using UnityEngine;
using System;

[Serializable]
public class StrategyForce : IMovementStrategy
{
    private Vector3 startPoint;
    private Vector3 finalPoint;
    private bool isMovingToStart; //falso
    private bool initialForce; //falso

    public StrategyForce(Vector3 start, Vector3 end)
    {
        startPoint = start;
        finalPoint = end;
    }
    public void Movement(Entity entity)
    {
        //sistema movimiento fuerza
        Rigidbody rb = entity.GetComponent<Rigidbody>();
        if(!initialForce)
        {
            ApplyForce(rb,isMovingToStart? startPoint: finalPoint, entity._force);
            initialForce = true;
        }
        if(Vector3.Distance(rb.position, isMovingToStart?startPoint:finalPoint) < 0.1f)
        {
            isMovingToStart = !isMovingToStart;
            ApplyForce(rb, isMovingToStart ? startPoint : finalPoint, entity._force);

        }

    }

    private void ApplyForce(Rigidbody rb, Vector3 target, float force)
    {
        if (rb != null)
        {
            if (isMovingToStart)
            {
                rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            Vector3 dir = (target - rb.position).normalized;
            rb.velocity = Vector3.zero;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}


