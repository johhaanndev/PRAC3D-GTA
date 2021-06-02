using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthBonus : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false;

    public GameObject ammoBox;


    public void TakeDamageBonus(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
                GetComponent<ZombieBonus>().anim.SetTrigger("Die");

                float lootProbability = Random.Range(0f, 100f);
                if (lootProbability >= 50)
                {
                    var ammo = (GameObject)Instantiate(ammoBox, transform.position, transform.rotation);
                }

                StartCoroutine(BuryAndDestroyBonus());
            }
        }
    }

    private IEnumerator BuryAndDestroyBonus()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<ZombieBonus>().burySpeed = 0.2f;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
