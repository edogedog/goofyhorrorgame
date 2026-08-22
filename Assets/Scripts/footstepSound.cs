using UnityEngine;
using UnityEngine.InputSystem;

public class footstepsSounds : MonoBehaviour
{
    public AudioSource walkSound;
    public AudioSource sprintSound;

    void Start()
    {
        walkSound.Stop();
        sprintSound.Stop();
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        bool moving =
            Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.dKey.isPressed;

        bool sprinting =
            moving && Keyboard.current.leftShiftKey.isPressed;

        ////// GÅR
        if (moving && !sprinting)
        {
            if (!walkSound.isPlaying)
                walkSound.Play();

            sprintSound.Stop();
        }

        ////// SPRINGER
        else if (sprinting)
        {
            if (!sprintSound.isPlaying)
                sprintSound.Play();

            walkSound.Stop();
        }

        ////// STÅR STILL
        else
        {
            walkSound.Stop();
            sprintSound.Stop();
        }
    }
}