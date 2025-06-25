using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 20;
    private bool canDamage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return;
        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }
        }
    }

    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;
}