using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEquipment
{
    protected Spell[] m_Spells;

    public Spell GetSpell(int spellSlot)
    {
        return m_Spells[spellSlot];
    }

    public void AddPassiveSpellsTo(EntityStats stats)
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
}
