using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStackedItems = 64;
    public int avalableSlots = 2;
    public int maxAvalableSlots = 8;
    public InventorySlot[] inventorySlots;
    public GameObject InventoryItemPrefab;
    public InventorySlot[] toolBarSlots;

    int selectedSlot = -1;


    private void Start()
    {
        for (int i = 0; i < toolBarSlots.Length; ++i)
        {
            toolBarSlots[i].gameObject.SetActive(false);
        }

        changeSlotAmount();
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < avalableSlots + 1)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    public void changeSlotAmount()
    {
        for (int i = 0; i < avalableSlots; ++i) 
        {
            toolBarSlots[i].gameObject.SetActive(true);
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].DeSelect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(ItemStats item)
    {
        for (int i = 0; i < inventorySlots.Length; ++i)
        {
            if (inventorySlots[i].gameObject.activeSelf)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null &&
                    itemInSlot.item == item &&
                    itemInSlot.count < maxStackedItems &&
                    itemInSlot.item.Stackable)
                {
                    ++itemInSlot.count;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }       
        } 

        for (int i = 0; i < inventorySlots.Length; ++i)
        {
            if (inventorySlots[i].gameObject.activeSelf)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot);
                    return true;
                }
            } 
        }

        return false;
    }

    void SpawnNewItem(ItemStats item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }
    
    public ItemStats GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            ItemStats item = itemInSlot.item;
            if (use)
            {
                --itemInSlot.count;
                if (itemInSlot.count <=0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }
        return null;
    }
}
