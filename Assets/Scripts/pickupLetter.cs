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
    
    // Cache for performance
    private AudioSource[] ambianceLayers;
    private AudioSource currentAmbianceLayer;
    private bool hasPlayedSound = false;
    private bool isInitialized = false;

    void Start()
    {
        // Initialize ambiance layers array for faster access
        ambianceLayers = new AudioSource[] { ambianceLayer1, ambianceLayer2, ambianceLayer3, ambianceLayer4, 
                                            ambianceLayer5, ambianceLayer6, ambianceLayer7, ambianceLayer8 };
        isInitialized = true;
    }

    void Update()
    {
        // Early exit if not initialized
        if (!isInitialized)
            return;
            
        // Check if the object is interactable and E key was pressed
        if (interactable && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Increment collected pages
            pagesCollected++;
            
            // Update collection text
            if (collectText != null)
                collectText.text = pagesCollected + "/8 pages";
            
            // Show collection text
            if (collectTextObj != null)
                collectTextObj.SetActive(true);
            
            // Play pickup sound once
            if (pickupSound != null && !hasPlayedSound)
            {
                pickupSound.Play();
                hasPlayedSound = true;
            }
            
            // Play ambiance layer based on collected pages
            if (pagesCollected >= 1 && pagesCollected <= 8)
            {
                // Stop any previously playing ambiance layer
                if (currentAmbianceLayer != null)
                {
                    currentAmbianceLayer.Stop();
                }
                
                // Play the new ambiance layer
                AudioSource layer = ambianceLayers[pagesCollected - 1];
                if (layer != null)
                {
                    layer.Play();
                    currentAmbianceLayer = layer;
                }
            }
            
            // Hide interact text
            if (intText != null)
                intText.SetActive(false);
            
            // Disable interactability
            interactable = false;
            
            // Deactivate the object
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