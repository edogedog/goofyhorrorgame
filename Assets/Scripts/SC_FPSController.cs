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
    
    // Cache for performance
    private CharacterController cachedController;
    private Camera cachedCamera;
    private bool isGroundedCache = false;
    private Vector3 groundPositionCache = Vector3.zero;
    private float groundPositionCacheTime = 0f;
    private bool isInputAvailable = false;
    
    Vector3 velocity;
    float rotationX;

    void Start()
    {
        cachedController = GetComponent<CharacterController>();
        
        if (playerCamera == null)
            playerCamera = Camera.main;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Check input availability once
        isInputAvailable = Keyboard.current != null && Mouse.current != null;
    }

    void Update()
    {
        // Early exit if input is not available
        if (!isInputAvailable)
            return;
        
        // Check if we can use cached ground check for better performance
        if (Time.time - groundPositionCacheTime > 0.1f)
        {
            // KOLLAR MARKEN
            groundPositionCache = transform.position + Vector3.down * (cachedController.height / 2f);
            isGroundedCache = Physics.CheckSphere(
                groundPositionCache,
                cachedController.radius * 0.9f,
                groundMask
            );
            groundPositionCacheTime = Time.time;
        }
        
        // WASD
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
        
        cachedController.Move(moveDirection * speed * Time.deltaTime);
        
        // HOPP
        if (isGroundedCache && velocity.y < 0f)
            velocity.y = -2f;
        
        if (isGroundedCache && Keyboard.current.spaceKey.wasPressedThisFrame)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        // GRAVITATION
        velocity.y += gravity * Time.deltaTime;
        cachedController.Move(velocity * Time.deltaTime);
        
        // MUS
        Vector2 mouse = Mouse.current.delta.ReadValue();
        
        // Multiply sensitivity for faster look speed
        float lookMultiplier = m_Sensitivity;   
        
        transform.Rotate(0f, mouse.x * lookSpeed * lookMultiplier, 0f);
        
        rotationX -= mouse.y * lookSpeed * lookMultiplier; // Negated to fix inverted movement
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        
        playerCamera.transform.localEulerAngles = new Vector3(rotationX, transform.eulerAngles.y, 0f);
    }
}