using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment
{   
    List<Item> m_Items;
    Spell[] m_Spells;
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

    public void AddPassiveSpellsTo(PlayerStats stats)
    {
        foreach (Spell spell in m_Spells)
        {
            if (spell.GetSpellType() == Spell.Types.passive || spell.GetSpellType() == Spell.Types.passiveSwitchable)
            {
                stats.ApplyPassiveSpell(spell);
            }
        }
        //return stats;
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

    public Spell GetSpell(int spellSlot)
    {
        return m_Spells[spellSlot];
    }
}
