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

    Image CircularBG => circularBackground;
    int itemID = -1;

    [SerializeField] UnityEvent onSelected;
    [SerializeField] UnityEvent onDeselected;

    private void OnEnable()
    {
        SetBGColor(baseColor);
    }

    public void Init(int itemID, float size)
    {
        SetCircularBG(size);
        this.itemID = itemID;
        
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
        onSelected?.Invoke();
    }

    public void Deselect()
    {
        SetBGColor(baseColor);
        onDeselected?.Invoke();
    }

    private void SetBGColor(Color color)
    {
        if(CircularBG != null)
        {
            CircularBG.color = color;
        }
    }
}
