using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class pickupLetter : MonoBehaviour
{
    public GameObject collectTextObj;
    public GameObject intText;

    public AudioSource pickupSound;

    public AudioSource ambianceLayer1;
    public AudioSource ambianceLayer2;
    public AudioSource ambianceLayer3;
    public AudioSource ambianceLayer4;
    public AudioSource ambianceLayer5;
    public AudioSource ambianceLayer6;
    public AudioSource ambianceLayer7;
    public AudioSource ambianceLayer8;

    public Text collectText;

    public static int pagesCollected = 0;

    [HideInInspector]
    public bool interactable = false;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E pressed");
        }

        if (interactable)
        {
            Debug.Log("Interactable");
        }
        if (!interactable)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            pagesCollected++;

            if (collectText != null)
                collectText.text = pagesCollected + "/8 pages";

            if (collectTextObj != null)
                collectTextObj.SetActive(true);

            if (pickupSound != null)
                pickupSound.Play();

            switch (pagesCollected)
            {
                case 1: if (ambianceLayer1) ambianceLayer1.Play(); break;
                case 2: if (ambianceLayer2) ambianceLayer2.Play(); break;
                case 3: if (ambianceLayer3) ambianceLayer3.Play(); break;
                case 4: if (ambianceLayer4) ambianceLayer4.Play(); break;
                case 5: if (ambianceLayer5) ambianceLayer5.Play(); break;
                case 6: if (ambianceLayer6) ambianceLayer6.Play(); break;
                case 7: if (ambianceLayer7) ambianceLayer7.Play(); break;
                case 8: if (ambianceLayer8) ambianceLayer8.Play(); break;
            }

            if (intText != null)
                intText.SetActive(false);

            interactable = false;

            gameObject.SetActive(false);
        }
    }

    public void SetInteractable(bool value)
    {
        interactable = value;

        if (intText != null)
            intText.SetActive(value);
    }
}