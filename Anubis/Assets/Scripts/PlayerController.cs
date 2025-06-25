using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [Header("Ground Check Settings")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;
    public InventoryManager inventoryManager;
    public QuickslotInventory quickslotInventory;

    public DamageScript swordDamageScript; // Призначте в інспекторі

    bool isGrounded;

    float ySpeed;
    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

    Vector2 moveInput;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        // inputActions.Enable();
    }

    private void OnDisable()
    {
        // inputActions.Disable();
    }

    private void Update()
    {
        if (animator.GetBool("Hit"))
            return;
        // Движение
        float h = 0f;
        float v = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) h -= 1f;
            if (Keyboard.current.dKey.isPressed) h += 1f;
            if (Keyboard.current.sKey.isPressed) v -= 1f;
            if (Keyboard.current.wKey.isPressed) v += 1f;
        }

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));
        var moveInputVector = (new Vector3(h, 0, v)).normalized;
        var moveDir = cameraController.PlanarRotation * moveInputVector;

        GroundCheck();
        if (isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (moveAmount > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
            rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);

        // Атака
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (quickslotInventory != null && quickslotInventory.activeSlot != null)
            {
                var item = quickslotInventory.activeSlot.item;
                if (item != null && item.itemType == ItemType.Instrument && !inventoryManager.isOpened)
                {
                    animator.SetBool("Hit", true);
                }
            }
        }
        // Не сбрасывай Hit на отпускание кнопки!
    }

    // Этот метод вызывается из Animation Event в конце анимации удара
    public void ResetHit()
    {
        animator.SetBool("Hit", false);
    }

    // Ці методи викликаються з Animation Event у анімації атаки
    public void EnableDamage()
    {
        if (swordDamageScript != null)
            swordDamageScript.EnableDamage();
    }

    public void DisableDamage()
    {
        if (swordDamageScript != null)
            swordDamageScript.DisableDamage();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}