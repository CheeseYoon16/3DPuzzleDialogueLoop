using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RequestItemManager : MonoBehaviour
{
    [SerializeField] PickableItemData requestedItem;

    public UnityEvent onCorrectItemGiven;

    public UnityEvent onFalseItemGiven;

    public void GiveItem(PickableItemData givenItem)
    {
        if (givenItem!=null)
        {
            if(givenItem == requestedItem)
            {
                onCorrectItemGiven.Invoke();
            }
            else
            {
                onFalseItemGiven.Invoke();
            }
        }
    }
}
