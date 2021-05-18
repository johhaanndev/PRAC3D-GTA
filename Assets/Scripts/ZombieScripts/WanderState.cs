using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IZombieState
{
    ZombieAI myZombie;
    private int nextWayPoint = 0;

    public WanderState(ZombieAI zombie)
    {
        myZombie = zombie;
    }

    public void UpdateState()
    {
        myZombie.navMeshAgent.destination = myZombie.waypoints[nextWayPoint].position;

        if (myZombie.navMeshAgent.remainingDistance <= myZombie.navMeshAgent.stoppingDistance)
        {
            nextWayPoint++;
            if (nextWayPoint >= myZombie.waypoints.Length)
            {
                nextWayPoint = 0;
            }
        }
    }

    public void GoToAttackState() { }

    public void GoToChaseState()
    {
        myZombie.currentState = myZombie.chaseState;
    }

    public void GoToWanderState() { }

    public void Impact()
    {
        GoToChaseState();
    }
}