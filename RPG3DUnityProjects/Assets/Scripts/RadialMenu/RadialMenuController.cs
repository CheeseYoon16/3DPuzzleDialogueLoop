using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RadialMenuController : MonoBehaviour
{
    [SerializeField] List<RadialMenuItemData> radialMenuOptions = new List<RadialMenuItemData>();
    [SerializeField] Transform parentTransform = null;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject detailGameobject;
    [SerializeField] RadialMenuItem itemOptionTemplate;
    [SerializeField] float gapWidth = 1.0f;

    float sizeDelta = 0;
    int prevSelectedIndex = -1;

    Dictionary<int, RadialMenuItem> currentActiveOptions = new Dictionary<int, RadialMenuItem>();

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
        sizeDelta = 360f/ radialMenuOptions.Count;
        float iconDistance = itemOptionTemplate.GetUprightGroupDistance();

        for(int i = 0; i < radialMenuOptions.Count; i++)
        {
            RadialMenuItem item = Instantiate(itemOptionTemplate, ParentTransform) as RadialMenuItem;

            //Set root element
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.Init(radialMenuOptions[i], i, 1f / radialMenuOptions.Count - gapWidth/360f);

            //Set rotating piece
            item.SetRotation(Quaternion.Euler(0, 0, -sizeDelta / 2f + gapWidth / 2f + i * sizeDelta), Quaternion.AngleAxis(i * sizeDelta, Vector3.forward) * Vector3.up * iconDistance);

        }
    }
}
