using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadialItemData_", menuName = "EliEntertainment/RadialMenu/Data")]
public class RadialMenuItemData : ScriptableObject
{
    [SerializeField] Sprite IconSprite;
    [SerializeField] string description;

    public Sprite Icon => IconSprite;

    public string Description => description;
}
