using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    public Animator animator;
    public Slider healthBar;

    void Start()
    {
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
        healthBar.value = currentHP;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHP <= 0) return;

        currentHP -= damageAmount;
        healthBar.value = currentHP;

        if (currentHP <= 0)
        {
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            healthBar.gameObject.SetActive(false);

            // Додаємо виклик дропа, якщо є EnemyArtifactDrop
            var artifactDrop = GetComponent<EnemyArtifactDrop>();
            if (artifactDrop != null)
            {
                artifactDrop.DropArtifact();
            }

            Destroy(gameObject, 10f);
        }
    }
}