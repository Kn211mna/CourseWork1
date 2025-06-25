using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    void Start()
    {
        healthBar.fillAmount = healthAmount / 100f;
    }

    void Update()
    {
        healthBar.fillAmount = healthAmount / 100f;
    }

    // Call this method to apply damage
    public void TakeDamage(float amount)
    {
        healthAmount -= amount;
        if (healthAmount < 0)
            healthAmount = 0;
    }
}