using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [HideInInspector]
    public IZombieState currentState;
    [HideInInspector]
    public WanderState wanderState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public AttackState attackState;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator anim;

    public Transform[] walkPoints;

    public GameObject player;

    public Transform hitPoint;
    public float attackRange = 2f;
    public LayerMask playerLayer;

    public float burySpeed = 0f;

    [Header("Others")]
    public ZombieHealth zombieHealth;
    public GameObject humanParticles;

    //public float wanderRadius;
    //public float wanderDistance;
    //public float wanderJitter;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        wanderState = new WanderState(this);
        chaseState = new ChaseState(this);
        attackState = new AttackState(this);

        currentState = wanderState;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        //Debug.Log("Health: " + zombieHealth.health);
        if (!zombieHealth.isDead)
        {
            currentState.UpdateState();
            if (currentState != attackState)
            {
                anim.SetBool("isAttacking", false);
            }

            if (currentState == wanderState)
            {
                navMeshAgent.speed = 0.7f;
            }
            if (currentState == chaseState)
            {
                navMeshAgent.speed = 1f;
            }
        }
        else
        {
            navMeshAgent.enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            transform.position =new Vector3(transform.position.x, transform.position.y - (burySpeed * Time.deltaTime), transform.position.z);
        }
    }

    public void resumeAgent()
    {
        navMeshAgent.Resume();
    }

    public void Hit(int damage)
    {
        if (!zombieHealth.isDead)
        {
            zombieHealth.TakeDamage(damage);
            currentState.Impact();
        }
    }

    private void CheckIfPlayerHit()
    {
        if (Physics.CheckSphere(hitPoint.position, attackRange, playerLayer))
        {
            Debug.Log("PlayerHit");
            var particles = (GameObject)Instantiate(humanParticles, hitPoint.position, hitPoint.rotation);
            player.GetComponent<PlayerHealth>().TakeDamage(25);
        }
        else
        {
            Debug.Log("miss");
        }
    }


    private void OnTriggerEnter(Collider coll)
    {
        if (currentState == wanderState)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                currentState = chaseState;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("car hit");
        }
    }
}
