using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToSell : MonoBehaviour
{
    public ItemStats item;
    public InventoryManager inventoryManager;
    public PlayerStatsScript playerStatsScript;

    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCost;
    public int money;
    public int cost;
    private void Start()
    {
        itemImage.sprite = item.image;
        itemName.text = item.name;
        itemCost.text = item.cost.ToString();
    }

    private void Update()
    {
        money = playerStatsScript.money;
        cost = item.cost;
    }

    public void buyItem()
    {
        if (cost <= money)
        {
            bool result = inventoryManager.AddItem(item);

            if (result)
            {
                Debug.Log("Item sold");
                playerStatsScript.money -= item.cost;
            }
            else
            {
                Debug.Log("Item was not sold");
            }
        }
        else
        {
            Debug.Log("Not enough money");
        }
        
    }
}
