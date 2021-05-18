using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseHumanState : IZombieState
{
    ZombieAI myZombie;

    public ChaseHumanState(ZombieAI zombie)
    {
        myZombie = zombie;
    }

    public void GoToAttackState()
    {
        throw new System.NotImplementedException();
    }

    public void GoToChaseState()
    {
        throw new System.NotImplementedException();
    }

    public void GoToChasePlayerState()
    {
        throw new System.NotImplementedException();
    }

    public void GoToWanderState()
    {
        throw new System.NotImplementedException();
    }

    public void Impact()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
