using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public bool isDead = false;

    private Animator anim;

    public GameObject hitFrame;
    public GameObject healthFrame;

    public AudioSource hurtSound;
    public AudioSource dieSound;

    public GameObject youDiedMenu;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        youDiedMenu.SetActive(false);

        isDead = false;
        health = 100;
        hitFrame.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        hitFrame.SetActive(true);
        health -= damage;
        if (health <= 0)
        {
            if (!isDead) dieSound.Play();
            health = 0;
            isDead = true;
            anim.SetTrigger("Die");
            GetComponent<CharacterController>().enabled = false;
            GetComponent<ShootingController>().enabled = false;
            youDiedMenu.SetActive(true);
        }
        else
        {
            hurtSound.Play();
        }
        Invoke(nameof(DisableHitFrame), 0.5f);
    }

    public void GetHealth(float healthAdded)
    {
        healthFrame.SetActive(true);
        health += healthAdded;
        if (health >= 100)
        {
            health = 100;
        }
        Invoke(nameof(DisableHealthFrame), 0.5f);
    }

    private void DisableHitFrame()
    {
        hitFrame.SetActive(false);
    }

    private void DisableHealthFrame()
    {
        healthFrame.SetActive(false);
    }
}
