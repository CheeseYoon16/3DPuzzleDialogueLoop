using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.VirtualTexturing;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent onInteracted;
    public void Interact()
    {
        Debug.Log($"Try to interact with {gameObject.name}");

        onInteracted?.Invoke();
    }
}
