using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25;
    public GameObject particleZombie;
    public GameObject particleSolid;

    private void OnCollisionEnter(Collision coll)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<TrailRenderer>().widthMultiplier = 0f;
        if (coll.gameObject.CompareTag("Zombie"))
        {
            coll.gameObject.GetComponent<ZombieAI>().Hit(damage);
            Instantiate(particleZombie, transform);
        }
        else if (coll.gameObject.CompareTag("ZombieBonus"))
        {
            coll.gameObject.GetComponent<ZombieBonus>().Hit(damage);
            Instantiate(particleZombie, transform);
        }
        else if (coll.gameObject.CompareTag("Human"))
        {
            coll.gameObject.GetComponent<HumanController>().HumanDie();
            Instantiate(particleZombie, transform);
        }
        else if (coll.gameObject.CompareTag("Goal"))
        {
            coll.gameObject.GetComponent<Gem>().GemImpact(5f);
            Instantiate(particleSolid, transform);
        }
        else
        {
            Instantiate(particleSolid, transform);
        }

        Invoke(nameof(DestroyGO), 2f);
    }



    private void DestroyGO()
    {
        Destroy(gameObject);
    } 
}
