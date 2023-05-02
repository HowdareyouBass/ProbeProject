using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEquipment
{
    protected Spell1[] m_Spells;

    public Spell1 GetSpell(int spellSlot)
    {
        return m_Spells[spellSlot];
    }

    public void AddPassiveSpellsTo(EntityStats stats)
    {
        foreach (Spell1 spell in m_Spells)
        {
            if (spell.GetSpellType() == Spell1.Types.passive || spell.GetSpellType() == Spell1.Types.passiveSwitchable)
            {
                stats.ApplyPassiveSpell(spell);
            }
        }
    }
}
