using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHUD : MonoBehaviour
{
    private Image healthBarImage;
    public PlayerHealth playerHealth;
    private float maxHealth = 100;
    private float currentHealth;

    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
    }

    private void Update()
    {
        currentHealth = playerHealth.health;
        //Debug.Log(currentHealth + " / " + maxHealth);
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }


}
