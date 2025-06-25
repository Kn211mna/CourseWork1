using UnityEngine;
using UnityEngine.UI; // або using TMPro; якщо використовуєте TextMeshPro

public class NPCHint : MonoBehaviour
{
    public GameObject hintTextObject; // Призначте UI Text або TextMeshProUGUI в інспекторі

    private void Start()
    {
        if (hintTextObject != null)
            hintTextObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hintTextObject != null)
                hintTextObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hintTextObject != null)
                hintTextObject.SetActive(false);
        }
    }
}