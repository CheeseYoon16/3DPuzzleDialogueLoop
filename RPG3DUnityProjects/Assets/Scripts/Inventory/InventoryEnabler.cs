using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryEnabler : MonoBehaviour
{
    public UnityEvent<PickableItemData> onItemSelectedEvent;

    public void EnableInventory()
    {
        if(InventorySystem.Instance != null)
        {
            InventorySystem.Instance.OpenInventory((x) =>
            {
                onItemSelectedEvent?.Invoke(x);
            });
        }
    }

    public void DisableInventory()
    {
        if (InventorySystem.Instance != null)
        {
            InventorySystem.Instance.CloseInventory();
        }
    }

}
