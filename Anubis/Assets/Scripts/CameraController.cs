//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class CameraController : MonoBehaviour
//{
//    [SerializeField] Transform followTarget;
//    [SerializeField] float rotationSpeed = 5f;
//    [SerializeField] float distance = 5;
//    [SerializeField] float minVerticalAngle = -45;
//    [SerializeField] float maxVerticalAngle = 45;
//    [SerializeField] Vector2 framingOffset;
//    [SerializeField] bool invertX;
//    [SerializeField] bool invertY;

//    float rotationX;
//    float rotationY;
//    float invertXVal;
//    float invertYVal;

//    public void LookAtTarget(Vector3 targetPosition)
//    {
//        Vector3 direction = targetPosition - transform.position;
//        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
//        rotationY = lookRotation.eulerAngles.y;
//        rotationX = lookRotation.eulerAngles.x;
//    }

//    public Transform GetFollowTarget()
//    {
//        return followTarget;
//    }

//    public void SetFollowTarget(Transform newTarget)
//    {
//        followTarget = newTarget;
//    }

//    private void Start()
//    {
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    private void Update()
//    {
//        invertXVal = (invertX) ? -1 : 1;
//        invertYVal = (invertY) ? -1 : 1;

//        Vector2 look = Vector2.zero;
//        if (Mouse.current != null)
//            look = Mouse.current.delta.ReadValue();

//        rotationX += look.y * invertYVal * rotationSpeed * Time.deltaTime;
//        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

//        rotationY += look.x * invertXVal * rotationSpeed * Time.deltaTime;

//        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
//        var focusPostion = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

//        transform.position = focusPostion - targetRotation * new Vector3(0, 0, distance);
//        transform.rotation = targetRotation;
//    }

//    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float rotationX;
    float rotationY;
    float invertXVal;
    float invertYVal;

    private Transform previousFollowTarget;

    public void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        rotationY = lookRotation.eulerAngles.y;
        rotationX = lookRotation.eulerAngles.x;
    }

    public Transform GetFollowTarget()
    {
        return followTarget;
    }

    public void SetFollowTarget(Transform newTarget)
    {
        followTarget = newTarget;
    }


    /// Фокусується на головоломці, зберігаючи попередню ціль.
    public void FocusOnPuzzle(Transform puzzleFocusPoint)
    {
        previousFollowTarget = followTarget;
        SetFollowTarget(puzzleFocusPoint);
        LookAtTarget(puzzleFocusPoint.position);
    }
    /// Повертає камеру до попередньої цілі.

    public void RestorePreviousTarget()
    {
        if (previousFollowTarget != null)
        {
            SetFollowTarget(previousFollowTarget);
            LookAtTarget(previousFollowTarget.position);
            previousFollowTarget = null;
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        Vector2 look = Vector2.zero;
        if (Mouse.current != null)
            look = Mouse.current.delta.ReadValue();

        rotationX += look.y * invertYVal * rotationSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += look.x * invertXVal * rotationSpeed * Time.deltaTime;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPostion = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        transform.position = focusPostion - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}











