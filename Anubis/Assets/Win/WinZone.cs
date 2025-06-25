using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameObject winPanel; // Призначте у інспекторі вашу панель "Ви виграли"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winPanel != null)
                winPanel.SetActive(true);
            // Можна додати ще: Time.timeScale = 0; // Зупинити гру
        }
    }
}