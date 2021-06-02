using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusZoneActivator : MonoBehaviour
{
    public GameObject bonusZombies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Start Bonus Level");
            bonusZombies.SetActive(true);
        }
    }
}
