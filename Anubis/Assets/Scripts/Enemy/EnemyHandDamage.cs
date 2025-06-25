using UnityEngine;

public class EnemyHandDamage : MonoBehaviour
{
    public float damage = 10f;
    private bool canDamage = false;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;
        HealthIndicator player = other.GetComponent<HealthIndicator>();
        if (player != null)
        {
            player.TakeDamage(damage);
            canDamage = false; // Щоб не наносити урон кілька разів за одну атаку
            GetComponent<Collider>().enabled = false;
        }
    }

    // Викликається з Animation Event у момент удару
    public void EnableDamageCollider()
    {
        canDamage = true;
        GetComponent<Collider>().enabled = true;
    }

    // Викликається з Animation Event у кінці атаки
    public void DisableDamageCollider()
    {
        canDamage = false;
        GetComponent<Collider>().enabled = false;
    }
}