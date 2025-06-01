using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    static InventorySystem instance;

    public static InventorySystem Instance => instance;

    public class InventoryItem
    {
        public string itemID = string.Empty;
        public int amount = 0;
        public PickableItemData itemData = null;

        public InventoryItem(PickableItemData itemData)
        {
            this.itemData = itemData;
        }
    }

    Dictionary<string, InventoryItem> inventoryItemsDictionary = new Dictionary<string, InventoryItem>();
    List<GameObject> slotItems = new List<GameObject>();
    Coroutine accessInventoryCorout = null;
    UnityAction<PickableItemData> onItemSelectedAction = null;

    private bool inventoryOpened
    {
        get 
        { 
            if(slotParents!=null)
            {
                return slotParents.gameObject.activeInHierarchy;
            }

            return false;
        }
    }

    [SerializeField] InventorySlot inventorySlotPrefab = null;
    [SerializeField] Transform slotParents;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(instance);
    }

    public void AddItemToInventory(PickableItemData pickableData)
    {
        if(pickableData!=null)
        {
            if(inventoryItemsDictionary == null)
            {
                inventoryItemsDictionary = new Dictionary<string, InventoryItem>();
            }

            if(!inventoryItemsDictionary.ContainsKey(pickableData.name))
            {
                inventoryItemsDictionary.Add(pickableData.name, new InventoryItem(pickableData));
            }

            InventoryItem existing = inventoryItemsDictionary[pickableData.name];
            if(existing != null)
            {
                inventoryItemsDictionary[pickableData.name].amount = existing.amount + 1;
                Debug.Log($"Add Pickable {pickableData} to inventory, current amount: {inventoryItemsDictionary[pickableData.name].amount}");
            }
        }
    }

    public void OpenInventory(UnityAction<PickableItemData> onSelectAction = null)
    {
        if (inventorySlotPrefab == null)
        {
            Debug.LogError($"INVENTORY SLOT PREFAB IS NULL, Add it to {gameObject.name} First!");
            return;
        }

        if (slotParents == null)
        {
            Debug.LogError($"SLOT PARENTS IS NULL, Add it to {gameObject.name} First!");
            return;
        }

        if(accessInventoryCorout==null)
        {
            if(onSelectAction != null)
            {
                onItemSelectedAction = onSelectAction;
            }

            accessInventoryCorout = StartCoroutine(OpenInventoryCorout());
        }
    }

    private IEnumerator OpenInventoryCorout()
    {
        foreach (var item in inventoryItemsDictionary)
        {
            if (item.Value.itemData != null && item.Value.amount > 0)
            {
                InventorySlot currentSlot = Instantiate(inventorySlotPrefab, slotParents);
                currentSlot.transform.position = Vector3.zero;
                currentSlot.Init(item.Value.itemData, (x) =>
                {
                    if(x != null)
                    {
                        Debug.Log($"Select item {x.name} from inventory");
                        onItemSelectedAction?.Invoke(x);
                    }

                });
                slotItems.Add(currentSlot.gameObject);
            }
        }

        yield return new WaitForSeconds(0.1f);

        slotParents.gameObject.SetActive(true);
        accessInventoryCorout = null;
    }

    public void CloseInventory()
    {
        if (slotParents == null)
        {
            Debug.LogError($"SLOT PARENTS IS NULL, Add it to {gameObject.name} First!");
            return;
        }

        if (accessInventoryCorout == null)
        {
            accessInventoryCorout = StartCoroutine(CloseInventoryCorout());
        }
    }

    private IEnumerator CloseInventoryCorout()
    {
        slotParents.gameObject.SetActive(false);
        ClearInventory();
        onItemSelectedAction = null;

        yield return new WaitForSeconds(0.1f);

        accessInventoryCorout = null;
    }

    private void ClearInventory()
    {
        for(int i = 0; i < slotItems.Count; i++)
        {
            if (slotItems[i].gameObject!=null)
            {
                Destroy(slotItems[i].gameObject);
            }
        }

        slotItems.Clear();
    }

    private void Update()
    {
        if(inventoryOpened && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    private void OnDisable()
    {
        ClearInventory();
    }

}
