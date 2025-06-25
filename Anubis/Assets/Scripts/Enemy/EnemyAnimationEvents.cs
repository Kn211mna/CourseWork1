using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    public EnemyHandDamage handDamage; // Призначте в інспекторі

    public void EnableDamageCollider()
    {
        if (handDamage != null)
            handDamage.EnableDamageCollider();
    }

    public void DisableDamageCollider()
    {
        if (handDamage != null)
            handDamage.DisableDamageCollider();
    }
}