using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IZombieState
{
    ZombieAI myZombie;
    public int nextWalkPoint = 0;
    //private Vector3 wanderTarget = Vector3.zero;

    public WanderState(ZombieAI zombie)
    {
        myZombie = zombie;
    }

    public void UpdateState()
    {
        myZombie.navMeshAgent.SetDestination(myZombie.walkPoints[nextWalkPoint].position);

        if (myZombie.navMeshAgent.remainingDistance <= myZombie.navMeshAgent.stoppingDistance * 1.5)
        {
            nextWalkPoint = UnityEngine.Random.Range(0, myZombie.walkPoints.Length - 1);
            //Debug.Log("nextWalkPoint: " + nextWalkPoint);
        }

        //wanderTarget += new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f) * myZombie.wanderJitter, 0, UnityEngine.Random.Range(-1.0f, 1.0f) * myZombie.wanderJitter);

        //wanderTarget.Normalize();
        //wanderTarget *= myZombie.wanderRadius;
        //Vector3 targetLocal = wanderTarget + new Vector3(0, 0, myZombie.wanderDistance); // target position of the Reynolds graph
        //Vector3 targetWorld = myZombie.gameObject.transform.InverseTransformVector(targetLocal); // actual position in the space ground

        //myZombie.navMeshAgent.SetDestination(targetWorld);
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