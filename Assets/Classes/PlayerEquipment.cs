using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment
{   
    List<Item> items;
    public PlayerEquipment()
    {
        items = new List<Item>();
    }
    public void EquipItem(Item eqipped)
    {
        items.Add(eqipped);
    }

    public int GetAttackSpeed()
    {
        int sum = 0;
        foreach (Item item in items)
        {
            sum += item.AttackSpeed;
        }
        return sum;
    }
}
