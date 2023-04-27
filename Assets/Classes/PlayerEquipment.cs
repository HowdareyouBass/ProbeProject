using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : EntityEquipment
{
    List<Item> m_Items;
    public PlayerEquipment()
    {
        m_Items = new List<Item>();
        m_Spells = new Spell[5];
        for (int i = 0; i < 5; i++)
        {
            m_Spells[i] = new Spell("");
        }
    }
    public void EquipItem(Item eqipped)
    {
        m_Items.Add(eqipped);
    }

    public void EquipSpell(Spell spell, int spellSlot)
    {
        m_Spells[spellSlot] = spell;
    }

    public int GetAttackSpeed()
    {
        int sum = 0;
        foreach (Item item in m_Items)
        {
            sum += item.GetAttackSpeed();
        }
        return sum;
    }
}
