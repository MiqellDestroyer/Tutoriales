using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField]
    private Database database; //Referencia a la base de datos
    [SerializeField]
    private GameObject slotPrefab; //Referencia al prefab del slot
    [SerializeField]
    private List<SlotInfo> slotInfoList; // Lista con la informacion de todos los slots (inventario propiamente dicho)
    [SerializeField]
    private int capacity; //capacidad de mi inventario

    private string jsonString; // Texto en formato json que usaremos para guardar el inventario.

    private void Start()
    {
        slotInfoList = new List<SlotInfo>();
        if (PlayerPrefs.HasKey("inventory"))
        {
            LoadSavedInventory();
        }
        else
        {
            LoadEmptyInventory();
        }
    }

    private void LoadEmptyInventory()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject slot = Instantiate<GameObject>(slotPrefab, this.transform);
            Slot newSlot = slotPrefab.GetComponent<Slot>();
            newSlot.SetUp(i);
            SlotInfo newSlotInfo = newSlot.slotInfo;
            slotInfoList.Add(newSlotInfo);
        }
    }

    private void LoadSavedInventory()
    {
        jsonString = PlayerPrefs.GetString("inventory");
        InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(jsonString);
        this.slotInfoList = inventoryWrapper.slotInfoList;
    }

    public SlotInfo FindItemInInventory(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.itemId == itemId && !slotInfo.isEmpty)
            {
                return slotInfo;
            }
        }
        return null;
    }

    private SlotInfo FindSuitableSlot(int itemId)
    {
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.itemId == itemId && slotInfo.amount < slotInfo.maxAmount && !slotInfo.isEmpty && database.FindItemInDatabase(itemId).isStackable)
            {
                return slotInfo;
            }
        }
        foreach (SlotInfo slotInfo in slotInfoList)
        {
            if (slotInfo.isEmpty)
            {
                slotInfo.EmptySlot();
                return slotInfo;
            }
        }
        return null;
    }

    public void AddItem(int itemId)
    {
        Item item = database.FindItemInDatabase(itemId); //buscar en la base de datos
        if (item != null)
        {
            SlotInfo slotInfo = FindSuitableSlot(itemId);
            if (slotInfo != null)
            {
                slotInfo.amount++;
                slotInfo.itemId = itemId;
                slotInfo.isEmpty = false;
            }
        }
    }

    public void RemoveItem(int itemId)
    {
        SlotInfo slotInfo = FindItemInInventory(itemId);
        if (slotInfo != null)
        {
            if (slotInfo.amount == 1)
            {
                slotInfo.EmptySlot();
            }
            else
            {
                slotInfo.amount--;
            }
        }
    }

    public void SaveInventory()
    {
        InventoryWrapper inventoryWrapper = new InventoryWrapper();
        inventoryWrapper.slotInfoList = this.slotInfoList;
        jsonString = JsonUtility.ToJson(inventoryWrapper);
        PlayerPrefs.SetString("inventory", jsonString);
    }

    private struct InventoryWrapper
    {
        public List<SlotInfo> slotInfoList;
    }

    [ContextMenu("Test Add - itemId = 1")]
    public void TestAdd()
    {
        AddItem(1);
    }
    [ContextMenu("Test Remove - itemId = 1")]
    public void TestRemove()
    {
        RemoveItem(1);
    }
    [ContextMenu("Test Save")]
    public void TestSave()
    {
        SaveInventory();
    }
}
