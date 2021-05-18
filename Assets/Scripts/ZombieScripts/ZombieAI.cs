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

    public GameObject player;
    public Transform[] waypoints;

    public Transform hitPoint;
    public float attackRange = 2f;
    public LayerMask playerLayer;

    [Header("Other scripts")]
    public ZombieHealth zombieHealth;

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
        }
        else
        {
            navMeshAgent.Stop();
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
}
