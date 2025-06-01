using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PickUpItem : MonoBehaviour
{
    [SerializeField] private PickableItemData pickableitemdata;
    [SerializeField] bool isDestroyedWhenPickedUp = false;

    public PickableItemData Pickable => pickableitemdata; //the object can be seen with another script but the value can't be changed or already define in this script.

    public void PickupItem()
    {
        if (pickableitemdata != null) 
        {
            Debug.Log($"{pickableitemdata.name} just picked up");

            //Add logic to add it to inventory here
            if(InventorySystem.Instance != null )
            {
                InventorySystem.Instance.AddItemToInventory(pickableitemdata);
            }

            if(isDestroyedWhenPickedUp)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
