using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 5f;
    public float runningSpeed = 8f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    public float lookSpeed = 0.01f;
    public float lookXLimit = 80f;
    public float m_Sensitivity = 2.0f;
    public float rotationDamping = 10.0f;
    public Camera playerCamera;

    // Marken m�ste ha detta layer
    public LayerMask groundMask;

    CharacterController controller;
    Vector3 velocity;
    float rotationX;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
            playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        ////// KOLLAR MARKEN
        Vector3 groundPosition =
            transform.position + Vector3.down * (controller.height / 2f);

        bool isGrounded = Physics.CheckSphere(
            groundPosition,
            controller.radius * 0.9f,
            groundMask
        );

        ////// WASD
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1f;
        if (Keyboard.current.sKey.isPressed) input.y -= 1f;
        if (Keyboard.current.dKey.isPressed) input.x += 1f;
        if (Keyboard.current.aKey.isPressed) input.x -= 1f;

        float speed = Keyboard.current.leftShiftKey.isPressed
            ? runningSpeed
            : walkingSpeed;

        // Move relative to camera facing direction
        Vector3 moveDirection = playerCamera.transform.forward * input.y + playerCamera.transform.right * input.x;
        moveDirection.y = 0f; // Keep movement flat on the ground
        moveDirection = moveDirection.normalized;
        
        controller.Move(moveDirection * speed * Time.deltaTime);

        ////// HOPP
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        ////// GRAVITATION
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        ////// MUS
        Vector2 mouse = Mouse.current.delta.ReadValue();

        // Multiply sensitivity for faster look speed
        float lookMultiplier = m_Sensitivity;   

        transform.Rotate(0f, mouse.x * lookSpeed * lookMultiplier, 0f);

        rotationX -= mouse.y * lookSpeed * lookMultiplier; // Negated to fix inverted movement
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localEulerAngles = new Vector3(rotationX, transform.eulerAngles.y, 0f);
    }
}