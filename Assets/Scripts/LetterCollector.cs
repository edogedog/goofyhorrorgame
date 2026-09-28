using UnityEngine;

public class LetterCollector : MonoBehaviour
{
    public float interactDistance = 3f;
    
    pickupLetter currentLetter;
    RaycastHit[] hitCache = new RaycastHit[1]; // Cache for raycast hits
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
            
        Ray ray = new Ray(transform.position, transform.forward);
        
        pickupLetter newLetter = null;
        
        // Use Physics.Raycast with cached hits for better performance
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            newLetter = hit.collider.GetComponent<pickupLetter>();
            
            if (newLetter == null)
                newLetter = hit.collider.GetComponentInParent<pickupLetter>();
        }
        
        if (newLetter != currentLetter)
        {
            if (currentLetter != null)
                currentLetter.SetInteractable(false);
            
            currentLetter = newLetter;
            
            if (currentLetter != null)
                currentLetter.SetInteractable(true);
        }
    }
}