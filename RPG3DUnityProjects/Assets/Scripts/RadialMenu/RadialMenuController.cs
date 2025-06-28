using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RadialMenuController : MonoBehaviour
{
    List<RadialMenuItem> radialMenuOptions = new List<RadialMenuItem>();
    [SerializeField] Transform parentTransform = null;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject detailGameobject;
    [SerializeField] float fillGap = 0.1f; // Gap in fill amount (0-1) between each menu item

    private Vector2 normalizedMousePosition;
    float currentAngle;
    float sizeDelta = 0;
    float angleStep = 0;

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

    [ContextMenu("Detect Radial Menu")]
    public void DetectRadialMenuItem()
    {
        radialMenuOptions = new List<RadialMenuItem>();

        for(int i = 0; i < transform.childCount; i++)
        {
            RadialMenuItem menuItem = transform.GetChild(i).GetComponent<RadialMenuItem>();

            if(menuItem!=null)
            {
                radialMenuOptions.Add(menuItem);
            }

        }

        GenerateMenu();
    }

    public void GenerateMenu()
    {
        // Calculate fill amount with gaps
        float totalGapFill = fillGap * radialMenuOptions.Count; // Total gap fill needed
        float availableFill = 1f - totalGapFill; // Available fill for items
        sizeDelta = availableFill / radialMenuOptions.Count; // Fill each item gets
        
        angleStep = 360f / radialMenuOptions.Count;
        currentActiveOptions = new Dictionary<int, RadialMenuItem>();

        for (int i = 0; i < radialMenuOptions.Count; i++)
        {
            radialMenuOptions[i].transform.localRotation = Quaternion.Euler(0, 0, angleStep * i - 90f);
            radialMenuOptions[i].Init(i, sizeDelta);


            if (!currentActiveOptions.ContainsKey(i))
            {
                currentActiveOptions.Add(i, radialMenuOptions[i]);
            }

            radialMenuOptions[i].onSelected?.AddListener((x) =>
            {
                SetDescription(x.Description);
            });
        }

        if(detailGameobject!=null)
        {
            detailGameobject.transform.SetAsLastSibling();
        }
    }

    private void Update()
    {
        normalizedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2); ; //make  the center in the middle of the screen
        currentAngle = currentAngle = (Mathf.Atan2(normalizedMousePosition.y, normalizedMousePosition.x) * Mathf.Rad2Deg + 360 + 90) % 360; //Get the angle in radians then convert them to degrees
        currentAngle = (currentAngle + 360) % 360; //add with 360 to make the angle always counter clockwise, modulo to prevent exceed 360

        SetRadialItemSelection(Mathf.FloorToInt(currentAngle / angleStep));
    }

    private void SetRadialItemSelection(int selectedIndex)
    {
        if (selectedIndex >= radialMenuOptions.Count || selectedIndex < 0)
        {
            return;
        }

        if (prevSelectedIndex >= 0)
        {
            if (prevSelectedIndex == selectedIndex)
            {
                return;
            }
            else if(prevSelectedIndex < radialMenuOptions.Count)
            {
                if (currentActiveOptions.TryGetValue(prevSelectedIndex, out RadialMenuItem item))
                {
                    item.Deselect();
                }
            }
        }


        if (currentActiveOptions.TryGetValue(selectedIndex, out RadialMenuItem selected))
        {
            selected.Select();
            prevSelectedIndex = selectedIndex;
        }

    }

    private void SetDescription(string text)
    {
        if(descriptionText != null)
        {
            descriptionText.text = text;
        }
    }
}
