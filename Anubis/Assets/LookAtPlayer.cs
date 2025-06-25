using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform camera;
    void Update()
    {
        transform.LookAt(camera.position);
    }
}
