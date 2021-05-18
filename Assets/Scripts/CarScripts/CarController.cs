using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRB;

    public bool isDriving;
    private float timer;
    private float minimumTimeToLeave = 0.5f;

    public GameObject player;
    public Transform leaveSpot;

    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDriving)
        {
            //drive
            Debug.Log("Driving from CarController");
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F) && timer >= minimumTimeToLeave)
            {
                timer = 0;
                isDriving = false;
                player.transform.position = leaveSpot.position;
                player.GetComponent<ShootingController>().enabled = true;
                player.GetComponent<CapsuleCollider>().enabled = true;
                player.GetComponent<PlayerMovement>().meshRenderers.enabled = true;
                player.transform.parent = null;
            }
        }
    }
}
