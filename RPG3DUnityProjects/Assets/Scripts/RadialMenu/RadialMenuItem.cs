using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class RadialMenuItem : MonoBehaviour
{
    [SerializeField] Image circularBackground;
    [SerializeField] Image iconHolder;

    [SerializeField] Color baseColor;
    [SerializeField] Color hoveredColor;
    [SerializeField] TextMeshProUGUI numberIndicator;

    [SerializeField] Transform rotatingGroup;
    [SerializeField] RectTransform uprightGroup;
    [SerializeField] float iconDistance = 100f;

    public UnityEvent<RadialMenuItemData> onSelected;
    public UnityEvent<RadialMenuItemData> onDeselected;

    Image CircularBG => circularBackground;

    int itemID = -1;
    RadialMenuItemData itemData = null;


    private void OnEnable()
    {
        SetBGColor(baseColor);
    }

    public void Init(RadialMenuItemData itemData, int itemID, float size)
    {
        SetCircularBG(size);
        this.itemID = itemID;
        this.itemData = itemData;

        SetItemAttribute();

        if (numberIndicator != null)
        {
            numberIndicator.text = itemID.ToString();
        }
    }

    private void SetCircularBG(float size)
    {
        if (CircularBG != null)
        {
            CircularBG.fillAmount = size;
        }
    }

    public void Select()
    {
        SetBGColor(hoveredColor);
        onSelected?.Invoke(itemData);
    }

    public void Deselect()
    {
        SetBGColor(baseColor);
        onDeselected?.Invoke(itemData);
    }

    private void SetBGColor(Color color)
    {
        if (CircularBG != null)
        {
            CircularBG.color = color;
        }
    }

    private void SetItemAttribute()
    {
        if (iconHolder != null)
        {
            iconHolder.sprite = itemData.Icon;
        }
    }

    public void SetRotation(Quaternion rotatingGroupRotation, Vector3 uprightGroupOffset)
    {
        if (rotatingGroup != null)
        {
            rotatingGroup.transform.localPosition = Vector3.zero;
            rotatingGroup.transform.localRotation = rotatingGroupRotation;

            if(uprightGroup != null)
            {
                uprightGroup.transform.localPosition = uprightGroupOffset;
            }
        }
    }

    public float GetUprightGroupDistance()
    {
        if (uprightGroup == null || rotatingGroup == null)
            return 0;

        return Vector3.Distance(uprightGroup.transform.position, rotatingGroup.transform.position);
    }
}

