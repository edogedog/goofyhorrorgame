using UnityEngine;
using UnityEngine.InputSystem;

public class flashlightMovement : MonoBehaviour
{
    public Animator flashlightAnim;
    
    // Cache for performance
    private bool lastMovingState = false;
    private bool lastShiftPressed = false;
    private bool isInitialized = false;

    void Start()
    {
        isInitialized = true;
    }

    void Update()
    {
        // Early exit if not initialized
        if (!isInitialized)
            return;
            
        // Early exit if no animator
        if (flashlightAnim == null)
            return;
            
        bool moving = 
            Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.dKey.isPressed;
            
        bool shiftPressed = Keyboard.current.leftShiftKey.isPressed;

        // Only process if state changed
        if (moving != lastMovingState || shiftPressed != lastShiftPressed)
        {
            if (moving)
            {
                if (shiftPressed)
                {
                    flashlightAnim.ResetTrigger("walk");
                    flashlightAnim.SetTrigger("sprint");
                }
                else
                {
                    flashlightAnim.ResetTrigger("sprint");
                    flashlightAnim.SetTrigger("walk");
                }
            }
            else
            {
                // Reset both triggers when not moving
                flashlightAnim.ResetTrigger("walk");
                flashlightAnim.ResetTrigger("sprint");
            }
            
            lastMovingState = moving;
            lastShiftPressed = shiftPressed;
        }
    }
}