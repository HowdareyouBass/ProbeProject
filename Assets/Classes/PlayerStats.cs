using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    float m_CurrentHealth;
    float m_MaxHealth;
    float m_Attack;
    float m_AttackRange;
    float m_AttackSpeed;
    float m_Evasion;

    public PlayerStats(Race race)
    {
        m_MaxHealth = race.health;
        m_CurrentHealth = race.health;
        m_Attack = race.attack;
        m_AttackRange = race.attackRange;
        m_AttackSpeed = race.baseAttackSpeed;
        m_Evasion = race.evasion;
    }

    public float GetAttackDamage()
    {
        return m_Attack;
    }
    
    public float GetAttackRange()
    {
        return m_AttackRange;
    }

    public float GetBaseAttackSpeed()
    {
        return m_AttackSpeed;
    }
}
