using UnityEngine;

public class EnemyProximityPush : MonoBehaviour
{
    public HealthIndicator playerHealthIndicator;
    public float damage = 10f;
    private bool playerInRange = false;

    void Start()
    {
        playerHealthIndicator = FindObjectOfType<HealthIndicator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // Цей метод викликається з анімації ворога у момент удару
    public void AttackPlayer()
    {
        if (playerInRange && playerHealthIndicator != null)
        {
            playerHealthIndicator.TakeDamage(damage);
        }
    }
}