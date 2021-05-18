using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;

    public void TakeDamage(int damage)
    {
        Debug.Log("took damage");
        if (!isDead)
        {
            Debug.Log("substract health");
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
                GetComponent<ZombieAI>().anim.SetTrigger("Die");
                Debug.Log("isDead from ZombieHealth.cs");
            }
        }
        else
        {
            // activate death animation
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
