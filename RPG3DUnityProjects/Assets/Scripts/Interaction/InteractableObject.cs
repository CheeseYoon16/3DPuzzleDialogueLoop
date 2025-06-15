using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.VirtualTexturing;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent onInteracted;
    [SerializeField] Material outlineMaterial;
    MeshRenderer meshRenderer;

    private void OnEnable()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        Debug.Log($"Try to interact with {gameObject.name}");

        onInteracted?.Invoke();
    }

    public void SetToInteractable(bool enable)
    {
        if (enable)
        {
            if (!HasOutlineMaterial())
            {
                Material[] currentMaterial = meshRenderer.materials;
                Material[] newMaterial = new Material[currentMaterial.Length + 1];
                currentMaterial.CopyTo( newMaterial, 0);
                newMaterial[newMaterial.Length - 1] = outlineMaterial;
                meshRenderer.materials = newMaterial;
            }
        }
        else
        {
            if(HasOutlineMaterial())
            {
                Material[] currentMaterial = meshRenderer.materials;
                List<Material> cleanMaterial = new List<Material>();

                for(int i = 0; i < currentMaterial.Length; i++)
                {
                    if (!currentMaterial[i].name.Contains(outlineMaterial.name))
                    {
                        cleanMaterial.Add(currentMaterial[i]);
                    }
                }

                meshRenderer.materials = cleanMaterial.ToArray();

            }
        }

    }

    private bool HasOutlineMaterial()
    {
        if(outlineMaterial!=null && meshRenderer!=null)
        {
            foreach (var mat in meshRenderer.materials)
            {
                if (mat.name.Contains(outlineMaterial.name))
                    return true;
            }
        }

        return false;

    }
}
