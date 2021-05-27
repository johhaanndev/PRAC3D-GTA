using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRunOver : MonoBehaviour
{
    public Rigidbody sphere;

    private void OnCollisionEnter(Collision collision)
    {
        if (sphere.velocity.magnitude > 0)
        {
            if (collision.gameObject.CompareTag("Zombie"))
            {
                Debug.Log("Zombie Hit");
            }
        }
    }
}
