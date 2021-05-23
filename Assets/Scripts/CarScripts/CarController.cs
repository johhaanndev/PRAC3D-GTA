﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRB;
    
    private float moveInput;
    private float turnInput;

    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;

    public bool isDriving;
    private float timer;
    private float minimumTimeToLeave = 0.5f;

    public GameObject player;
    public Transform leaveSpot;

    public LayerMask zombieLayer;

    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        if (player.GetComponent<PlayerMovement>().isDriving)
        {
            timer += Time.deltaTime;
            moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;

            float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            transform.Rotate(0f, newRotation, 0f, Space.World);

            DeactivatePlayerComponents();
            player.transform.position = transform.position;
            player.transform.parent = transform;

            if (Input.GetKeyDown(KeyCode.F) && timer >= minimumTimeToLeave)
            {                
                timer = 0;

                Debug.Log("You would get out of car");
                player.GetComponent<PlayerMovement>().isDriving = false;
                player.GetComponent<ShootingController>().enabled = true;
                player.GetComponent<CapsuleCollider>().enabled = true;
                player.GetComponent<PlayerMovement>().meshRenderers.enabled = true;
                transform.position = leaveSpot.position;
                player.transform.parent = null;
            }
        }

        transform.position = sphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }

    private void DeactivatePlayerComponents()
    {
        player.GetComponent<ShootingController>().enabled = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        player.GetComponent<PlayerMovement>().meshRenderers.enabled = false;
    }
}
