using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IZombieState
{
    ZombieAI myZombie;

    float actualTimeBetweenSlaps = 1f;

    public AttackState(ZombieAI zombie)
    {
        myZombie = zombie;
    }

    public void UpdateState()
    {
        myZombie.transform.LookAt(myZombie.player.transform);
        if (Mathf.Abs((myZombie.player.transform.position - myZombie.transform.position).magnitude) >= myZombie.navMeshAgent.stoppingDistance)
        {
            Debug.Log("Player ran away");
            GoToChaseState();
        }

        myZombie.anim.SetBool("isAttacking", true);
    }

    public void GoToAttackState() { }

    public void GoToWanderState() { }

    public void Impact() { }
    
    public void GoToChaseState()
    {
        myZombie.navMeshAgent.Resume();
        myZombie.currentState = myZombie.chaseState;
    }
}
