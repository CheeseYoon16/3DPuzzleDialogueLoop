using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class RadialMenuItem : MonoBehaviour
{
    [SerializeField] RadialMenuItemData itemData = null;

    [SerializeField] Image circularBackground;
    [SerializeField] Image iconHolder;

    [SerializeField] Color baseColor;
    [SerializeField] Color hoveredColor;
    [SerializeField] TextMeshProUGUI numberIndicator;
    [SerializeField] Transform uprightGroup;

    public UnityEvent<RadialMenuItemData> onSelected;
    public UnityEvent<RadialMenuItemData> onDeselected;

    Image CircularBG => circularBackground;
    int itemID = -1;


    private void OnEnable()
    {
        SetBGColor(baseColor);
    }

    public void Init(int itemID, float size)
    {
        SetCircularBG(size);
        this.itemID = itemID;

        SetItemAttribute();

        if(numberIndicator != null )
        {
            numberIndicator.text = itemID.ToString();
        }
    }

    private void SetCircularBG(float size)
    {
        if(CircularBG != null)
        {
            Debug.Log($"size delta is: {size}");
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
        if(CircularBG != null)
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

}
