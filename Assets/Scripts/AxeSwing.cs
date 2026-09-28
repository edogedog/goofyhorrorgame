using UnityEngine;
using UnityEngine.InputSystem;

public class AxeController : MonoBehaviour
{
    public Animator Axe;
    private bool isSwinging = false;
    private bool canSwing = true;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canSwing)
        {
            
            isSwinging = true;
            canSwing = false;
            // Immediately start a new swing animation
            Axe.ResetTrigger("attack");
            Axe.SetTrigger("attack");
        }
    }

    void LateUpdate()
    {
        if (isSwinging)
        {
            // Check if the animation has completed
            AnimatorStateInfo stateInfo = Axe.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("idle"))
            {
                isSwinging = false;
                canSwing = true;
            }
        }
    }
}