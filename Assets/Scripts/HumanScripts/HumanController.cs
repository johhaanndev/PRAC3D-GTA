using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform[] waypoints;
    private int nextWaypoint = 0;

    public bool isFleeing = false;
    public float safeDistance = 15f;

    private GameObject zombieToRunAway = null;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFleeing)
        {
            WalkingAround();
        }
        else
        {
            Flee();
        }
    }

    private void Flee()
    {
        float distanceFromZombie = Mathf.Abs(Vector3.Distance(transform.position, zombieToRunAway.transform.position));

        Vector3 dirToFlee = transform.position - zombieToRunAway.transform.position;
        Vector3 newPos = transform.position + dirToFlee;

        agent.speed = 4;
        agent.SetDestination(newPos);

        if (distanceFromZombie >= safeDistance)
        {
            agent.speed = 2f;
            isFleeing = false;
        }
    }

    private void WalkingAround()
    {
        agent.destination = waypoints[nextWaypoint].position;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            nextWaypoint++;
            if (nextWaypoint >= waypoints.Length)
            {
                nextWaypoint = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie close");
            zombieToRunAway = other.gameObject;
            isFleeing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Zombie close");
            zombieToRunAway = other.gameObject;
            isFleeing = true;
        }
    }
}
