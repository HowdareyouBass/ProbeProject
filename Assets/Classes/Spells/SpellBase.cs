using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBase
{
    float m_SpellDamage;

    public void SetSpellStats(Spell spellStats)
    {
        m_SpellDamage = spellStats.Damage;
    }

    public virtual GameObject CastEffect()
    {
        Debug.Log("Empty or passive");
        return new GameObject();
    }

    public float GetDamage()
    {
        return m_SpellDamage;
    }
}
