using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadialMenuController : MonoBehaviour
{
    [SerializeField] List<RadialMenuItem> radialMenuOptions = new List<RadialMenuItem>();
    [SerializeField] Transform parentTransform = null;

    public Vector2 normalizedMousePosition;
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

    private void OnEnable()
    {
        GenerateMenu();
    }

    public void GenerateMenu()
    {
        sizeDelta = 1f / radialMenuOptions.Count;
        angleStep = 360f / radialMenuOptions.Count;
        currentActiveOptions = new Dictionary<int, RadialMenuItem>();

        for (int i = 0; i < radialMenuOptions.Count; i++)
        {
            RadialMenuItem spawned = Instantiate(radialMenuOptions[i], ParentTransform);
            spawned.Init(i, sizeDelta);
            spawned.transform.localRotation = Quaternion.Euler(0, 0, angleStep * i);

            if(!currentActiveOptions.ContainsKey(i))
            {
                currentActiveOptions.Add(i, spawned);
            }
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
                    Debug.Log($"attempt deselect {prevSelectedIndex}");
                    item.Deselect();
                }
            }
        }


        if (currentActiveOptions.TryGetValue(selectedIndex, out RadialMenuItem selected))
        {
            selected.Select();
            prevSelectedIndex = selectedIndex;
        }

        Debug.Log($"current selected: {selectedIndex}");
    }
}
