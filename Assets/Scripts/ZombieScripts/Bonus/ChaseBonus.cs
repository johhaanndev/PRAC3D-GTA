using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBonus : IZombieState
{
    ZombieBonus myZombie;

    public ChaseBonus(ZombieBonus zombie)
    {
        myZombie = zombie;
    }

    public void UpdateState()
    {
        myZombie.navMeshAgent.destination = myZombie.player.transform.position;
        if (myZombie.navMeshAgent.remainingDistance <= myZombie.navMeshAgent.stoppingDistance)
        {
            Debug.Log("Player reached: attack!");
            GoToAttackState();
        }
    }

    public void GoToAttackState()
    {
        myZombie.navMeshAgent.Stop();
        myZombie.currentState = myZombie.attackState;
    }

    public void GoToChasePlayerState() { }

    public void GoToWanderState() { }

    public void Impact() { }

    public void GoToChaseState()
    {
        throw new System.NotImplementedException();
    }
}
