using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuController : MonoBehaviour
{
    [SerializeField] List<RadialMenuItem> radialMenuOptions = new List<RadialMenuItem>();
    [SerializeField] Transform parentTransform = null;

    Transform ParentTransform 
    { 
        get 
        { 
            if (parentTransform == null)
            { 
                return transform; 
            }

            return parentTransform;
        } 
    }

    private void OnEnable()
    {
        GenerateMenu();
    }

    public void GenerateMenu()
    {
        float sizeDelta = 1f/ radialMenuOptions.Count;
        float angleStep = 360f / radialMenuOptions.Count;

        for (int i = 0; i < radialMenuOptions.Count; i++)
        {
            RadialMenuItem spawned = Instantiate(radialMenuOptions[i], ParentTransform);
            spawned.Init(sizeDelta);
            spawned.transform.localRotation = Quaternion.Euler(0, 0, -angleStep * i);
        }
    }
}
