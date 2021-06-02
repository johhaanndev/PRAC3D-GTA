using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBonus : MonoBehaviour
{
    [HideInInspector]
    public IZombieState currentState;
    [HideInInspector]
    public ChaseBonus chaseState;
    [HideInInspector]
    public AttackBonus attackState;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator anim;

    public GameObject player;

    public Transform hitPoint;
    public float attackRange = 2f;
    public LayerMask playerLayer;

    public float burySpeed = 0f;

    [Header("Others")]
    public ZombieHealthBonus zombieHealth;
    public GameObject humanParticles;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        chaseState = new ChaseBonus(this);
        attackState = new AttackBonus(this);

        currentState = chaseState;
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
            if (currentState == chaseState)
            {
                navMeshAgent.speed = 2f;
            }
        }
        else
        {
            navMeshAgent.enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            transform.position = new Vector3(transform.position.x, transform.position.y - (burySpeed * Time.deltaTime), transform.position.z);
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
            zombieHealth.TakeDamageBonus(damage);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("car hit");
        }
    }
}
