using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentBuilding : MonoBehaviour
{
    public GameObject building;

    public Material buildingMaterial;
    public Material transparentMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            building.GetComponent<MeshRenderer>().material = transparentMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            building.GetComponent<MeshRenderer>().material = buildingMaterial;
        }
    }
}
