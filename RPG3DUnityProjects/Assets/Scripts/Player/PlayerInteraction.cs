using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;

    private InteractableObject detectedObject;

    private InteractableObject currentInteractable
    {
        get
        {
            return detectedObject;
        }
        set
        {
            if(value!=null)
            {
                Debug.Log($"Detected interactible object {value.gameObject.name}");
            }

            if(detectedObject != null)
            {
                if (detectedObject != value)
                {
                    detectedObject.SetToInteractable(false);
                }
            }

            detectedObject = value;

            if (detectedObject != null)
            {
                detectedObject.SetToInteractable(true);
            }
        }
    }

    bool disabledInteraction= false;

    void DetectObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionRadius);
        InteractableObject foundInteractable = null;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out InteractableObject targetObject))
            {
                foundInteractable = targetObject;
                break;
            }
        }

        // Show or hide prompt based on presence
        ShowPrompt(foundInteractable != null);

        // Assign detected interactable (this will automatically handle outline toggling)
        currentInteractable = foundInteractable;

    }

    private void Interact()
    {
        if(currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void ShowPrompt(bool show)
    {
        if(InteractButtonPrompt.Instance!= null)
        {
            InteractButtonPrompt.Instance.ShowPrompt(show);
        }
    }

    private void Update()
    {
        if(!disabledInteraction)
        {
            DetectObject();

            if (currentInteractable != null && Input.GetKeyDown(KeyCode.E)) //If there is object to interact, then i can interact
            {
                Interact();
            }
        }

    }

    public void SetInteractionDisabled(bool active)
    {
        disabledInteraction = active;
    }

}
