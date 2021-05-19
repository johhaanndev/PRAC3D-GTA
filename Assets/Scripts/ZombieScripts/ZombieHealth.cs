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
                StartCoroutine(BuryAndDestroy());
            }
        }
    }

    private IEnumerator BuryAndDestroy()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<ZombieAI>().burySpeed = 0.2f;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
