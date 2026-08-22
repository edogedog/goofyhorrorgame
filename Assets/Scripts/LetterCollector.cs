using UnityEngine;

public class LetterCollector : MonoBehaviour
{
    public float interactDistance = 3f;

    pickupLetter currentLetter;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        pickupLetter newLetter = null;

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