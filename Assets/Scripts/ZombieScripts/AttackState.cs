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
            GoToChasePlayerState();
        }
    }

    public void GoToAttackState()
    {
        throw new System.NotImplementedException();
    }

    public void GoToChasePlayerState()
    {
        myZombie.navMeshAgent.Resume();
        myZombie.currentState = myZombie.chaseState;
    }

    public void GoToWanderState()
    {
        throw new System.NotImplementedException();
    }

    public void Impact() { }
    
    private void ResumeAgent()
    {
        myZombie.navMeshAgent.Resume();
    }

    public void GoToChaseState()
    {
        throw new System.NotImplementedException();
    }
}
