using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Spell
{
    public enum Types
    {
        none, projectile, directedAtEnemy, directedAtGround, playerCast, custom
    }
    [SerializeField] private string m_Name;
    [SerializeField] private float m_SpellDamage;
    [SerializeField] private GameObject m_Effect;    
    public Types type = Types.none;
    public float GetDamage()
    {
        return m_SpellDamage;
    }

    public GameObject GetEffect()
    {
        return m_Effect;
    }

    public string GetName()
    {
        return m_Name;
    }
}
