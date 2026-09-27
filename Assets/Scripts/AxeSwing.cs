using UnityEngine;
using UnityEngine.InputSystem;

public class AxeController : MonoBehaviour
{
    public Animator Axe;

    void Update()
    {
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            Axe.SetTrigger("kill");
        }
    }
}