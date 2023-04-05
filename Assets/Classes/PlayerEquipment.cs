using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerEquipment
{   
    List<Item> m_Items;
    SpellBase[] m_Spells;
    public PlayerEquipment()
    {
        m_Items = new List<Item>();
        m_Spells = new SpellBase[5];
    }
    public void EquipItem(Item eqipped)
    {
        m_Items.Add(eqipped);
    }

    public void EquipSpell(SpellBase spell, int spellSlot)
    {
        m_Spells[0] = spell;
    }

    public int GetAttackSpeed()
    {
        int sum = 0;
        foreach (Item item in m_Items)
        {
            sum += item.AttackSpeed;
        }
        return sum;
    }

    public SpellBase GetSpell(int spellSlot)
    {
        return m_Spells[spellSlot];
    }
}
