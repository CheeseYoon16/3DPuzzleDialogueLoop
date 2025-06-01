using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickableItemData", menuName = "EliEntertainment/PickableItemData")]
public class PickableItemData : ScriptableObject
{
    [SerializeField] Sprite iconImage;

    public Sprite Icon => iconImage;

}