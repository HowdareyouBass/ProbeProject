using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInventoryScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemStats[] itemsToPick;
    
    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPick[id]);

        if (result)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item was not added");
        }
    }

    public void GetSelectedItem()
    {
        ItemStats recieveItem = inventoryManager.GetSelectedItem(false);
        if (recieveItem)
        {
            Debug.Log("Recieved item" + recieveItem);
        }
        else
        {
            Debug.Log("No item recieved");
        }
    }

    public void UseSelectedItem()
    {
        ItemStats recievedItem = inventoryManager.GetSelectedItem(true);
        if (recievedItem)
        {
            Debug.Log("Item used" + recievedItem);
        }
        else
        {
            Debug.Log("No item used");
        }
    }
}
