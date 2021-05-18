using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
                GetComponent<ZombieAI>().anim.SetTrigger("Die");
            }
        }
        else
        {
            // activate death animation
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

}
