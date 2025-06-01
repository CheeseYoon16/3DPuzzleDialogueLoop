using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    private InteractableObject currentInteractable = null;
    bool disabledInteraction= false;

    void DetectObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        { 
            if (hitCollider.GetComponent<InteractableObject>() != null) //this to check if the object with collider can be interacted
            {
                InteractableObject targetObject = hitCollider.GetComponent<InteractableObject>(); //this to get the script or logic that handles the interaction.

                if (currentInteractable != null)
                {
                    if (targetObject != currentInteractable)
                    {
                        ShowPrompt(false);
                    }
                }

                ShowPrompt(true);
                currentInteractable = targetObject;
                break;
            }

            currentInteractable = null;
            ShowPrompt(false);
        }
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
