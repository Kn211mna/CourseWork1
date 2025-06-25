using UnityEngine;

public class EnemyArtifactDrop : MonoBehaviour
{
    public GameObject artifactPrefab; // Призначте артефакт у інспекторі

    // Цей метод можна викликати з EnemyScript при смерті ворога
    public void DropArtifact()
    {
        if (artifactPrefab != null)
        {
            Instantiate(artifactPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}