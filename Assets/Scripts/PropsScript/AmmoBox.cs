using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Disappear), 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ShootingController>().AddAmmo(Random.Range(20, 40));
            Destroy(gameObject);
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }

}
