using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25;

    private void OnCollisionEnter(Collision coll)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        if (coll.gameObject.CompareTag("Zombie"))
        {
            coll.gameObject.GetComponent<ZombieAI>().Hit(damage); ;
        }

        Invoke(nameof(DestroyGO), 2f);
    }



    private void DestroyGO()
    {
        Destroy(gameObject);
    } 
}
