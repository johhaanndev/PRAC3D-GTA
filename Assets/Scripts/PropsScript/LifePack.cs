using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePack : MonoBehaviour
{

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f));
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<PlayerHealth>().GetHealth(25);
            Destroy(gameObject);
        }
    }
}
