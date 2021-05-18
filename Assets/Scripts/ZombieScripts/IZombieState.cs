using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieState
{
    void UpdateState();
    void GoToAttackState();
    void GoToChaseState();
    void GoToWanderState();
    void Impact();
}
