using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


[Serializable]
public class StrategyWaypoints : IMovementStrategy
{
    private readonly List<Transform> waypoints;
    private int currentWayPointIndex;
    private bool isForward = true;

    

    public StrategyWaypoints(List<Transform> waypoints)
    {
        this.waypoints = waypoints;
        currentWayPointIndex = 0;
    }

    public void Movement(Entity entity)
    {
        if (waypoints.Count == 0) return;

        Transform target = waypoints[currentWayPointIndex];
        entity.transform.position = Vector3.MoveTowards(entity.transform.position, target.position, entity._speed*Time.deltaTime);

        if(Vector3.Distance(entity.transform.position,target.position)<0.1f)
        {
            if(isForward)
            {   
                entity.transform.rotation = Quaternion.Euler(0, 180, 0);
                if (currentWayPointIndex<waypoints.Count-1)
                {
                    currentWayPointIndex++;
                }
                else
                {

                    isForward = false;
                    currentWayPointIndex--;
                }
            }
            else 
            {
                entity.transform.rotation = Quaternion.Euler(0, 0, 0);

                if (currentWayPointIndex > 0)
                {
                    currentWayPointIndex--;
                }
                else
                {
                    isForward = true;
                    currentWayPointIndex++;
                }
            }
        }


    }


}
