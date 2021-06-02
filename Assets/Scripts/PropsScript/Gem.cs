using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float gemHealth = 100;
    public GoalManager goalManager;

    public GameObject explodeParticles;

    public void GemImpact(float damage)
    {
        gemHealth -= damage;
        if (gemHealth <= 0)
        {
            var particles = (GameObject)Instantiate(explodeParticles, transform.position, transform.rotation);
            goalManager.GemDestroyed();
            Destroy(gameObject);
        }
    }
}
