using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterExit : MonoBehaviour
{
    public GameObject player;
    public Transform motorSphere;
    public Transform leaveSpot;

    private float timer;
    private float minimTimeToExitCar = 0.5f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().isDriving)
        {
            timer += Time.deltaTime;

            DeactivatePlayerComponents();
            player.transform.position = motorSphere.transform.position;
            if (timer >= minimTimeToExitCar)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    timer = 0;
                    player.transform.position = leaveSpot.position;
                    ActivatePlayerComponents();
                    GetComponent<CarMovement>().enabled = false;
                    this.enabled = false;
                }
            }
        }
    }

    private void DeactivatePlayerComponents()
    {
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<PlayerMovement>().meshRenderers.enabled = false;
        player.GetComponent<ShootingController>().enabled = false;
        player.transform.parent = transform.parent;
    }

    private void ActivatePlayerComponents()
    {
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<CapsuleCollider>().enabled = true;
        player.GetComponent<PlayerMovement>().meshRenderers.enabled = true;
        player.GetComponent<PlayerMovement>().isDriving = false;
        player.GetComponent<ShootingController>().enabled = true;
        player.transform.parent = null;
    }
}
