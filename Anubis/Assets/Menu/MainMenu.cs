using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Викликається кнопкою "Грати"
    public void PlayGame()
    {
        // Замініть "GameScene" на назву вашої ігрової сцени
        SceneManager.LoadScene("Levl_1");
    }

    // Можна додати інші методи, наприклад, для виходу з гри
    public void QuitGame()
    {
        Application.Quit();
    }
}