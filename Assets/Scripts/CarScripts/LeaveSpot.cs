using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSpot : MonoBehaviour
{
    public Transform car;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(car.position.x - 4f, car.position.y, car.position.z);
    }
}
