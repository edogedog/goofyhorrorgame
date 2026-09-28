using UnityEngine;
using UnityEngine.InputSystem;

public class footstepsSounds : MonoBehaviour
{
    public AudioSource walkSound;
    public AudioSource sprintSound;
    
    private bool isInitialized = false;

    void Start()
    {
        walkSound.Stop();
        sprintSound.Stop();
        isInitialized = true;
    }

    void Update()
    {
        // Early exit if not initialized
        if (!isInitialized)
            return;
            
        if (Keyboard.current == null)
            return;

        bool moving =
            Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.dKey.isPressed;

        bool sprinting =
            moving && Keyboard.current.leftShiftKey.isPressed;

        ////// G�R
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

        ////// ST�R STILL
        else
        {
            walkSound.Stop();
            sprintSound.Stop();
        }
    }
}