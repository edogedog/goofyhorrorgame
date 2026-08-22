using UnityEngine;
using UnityEngine.InputSystem;

public class flashlightMovement : MonoBehaviour
{
    public Animator flashlightAnim;

    void Update()
    {
        bool moving =
            Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.dKey.isPressed;

        if (moving)
        {
            if (Keyboard.current.leftShiftKey.isPressed)
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
    }
}