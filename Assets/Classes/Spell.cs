using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Spell
{
    public enum Types { none, projectile, directedAtEnemy, directedAtGround, playerCast, custom }
    [SerializeField] private string m_Name;
    [SerializeField] private float m_SpellDamage;
    [SerializeField] private GameObject m_Effect;
    [SerializeField] private GameObject m_EffectOnImpact;
    [SerializeField] private int m_SpeedOfProjectile;
    [SerializeField] private Types m_Type = Types.none;

    public Spell(string name)
    {
        m_Name = name;
    }

    public string GetName()
    {
        return m_Name;
    }
    public float GetDamage()
    {
        return m_SpellDamage;
    }

    public GameObject GetEffect()
    {
        return m_Effect;
    }

    public GameObject GetEffectOnImpact()
    {
        return m_EffectOnImpact;
    }

    public int GetSpeed()
    {
        return m_SpeedOfProjectile;
    }
    public Types GetSpellType()
    {
        return m_Type;
    }
}
