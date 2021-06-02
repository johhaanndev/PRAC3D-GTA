using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    public Transform[] waypoints;
    private int nextWaypoint = 0;

    public bool isFleeing = false;
    public float safeDistance = 15f;

    public bool isDead = false;
    public float burySpeed = 0f;

    private GameObject zombieToRunAway = null;
    public GameObject zombieToSpawn;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        if (!isDead)
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
            zombieToRunAway = other.gameObject;
            isFleeing = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            zombieToRunAway = other.gameObject;
            isFleeing = true;
        }
    }

    public void HumanDie()
    {
        isDead = true;
        agent.Stop();
        anim.SetTrigger("Die");
        StartCoroutine(BuryInstantiateZombieAndDestroy());
    }

    private IEnumerator BuryInstantiateZombieAndDestroy()
    {
        yield return new WaitForSeconds(3f);
        var zombie = (GameObject)Instantiate(zombieToSpawn, transform.position, transform.rotation);
        zombie.GetComponent<ZombieAI>().player = GameObject.Find("Player");
        zombie.GetComponent<ZombieAI>().playerLayer = playerLayer;
        zombie.GetComponent<ZombieAI>().walkPoints = waypoints;
        Destroy(gameObject);
    }
}
