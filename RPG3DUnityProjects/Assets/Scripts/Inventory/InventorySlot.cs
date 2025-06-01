using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image slotIconImage;
    [SerializeField] Button slotButton;
    PickableItemData pickableItemData = null;

    public UnityEvent<PickableItemData> onSelectItem = new UnityEvent<PickableItemData>();

    public void Init(PickableItemData pickableItem, UnityAction<PickableItemData> selectedAction)
    {
        if(pickableItem == null)
        {
            return;
        }

        pickableItemData = pickableItem;
        if(pickableItemData.Icon !=null)
        {
            if (slotIconImage != null)
            {
                slotIconImage.sprite = pickableItemData.Icon;
            }
        }

        RegisterOnSelectItem(selectedAction);
        if (slotButton != null)
        {
            slotButton.onClick.AddListener(ClickItem);
        }
    }

    private void RegisterOnSelectItem(UnityAction<PickableItemData> selectedAction)
    {
        onSelectItem?.AddListener(selectedAction);
    }

    private void ClickItem()
    {
        onSelectItem?.Invoke(pickableItemData);

        //the line above is a shortcut for:
        /*
        if(onSelectItem != null)
        {
            onSelectItem.Invoke(pickableItemData);
        }*/
    }

    private void OnDisable()
    {
        onSelectItem?.RemoveAllListeners();
    }
}
