using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    float m_CurrentHealth;
    float m_MaxHealth;
    float m_Attack;
    public float m_AttackRange;
    float m_Evasion;

    public void setRace(Race race)
    {
        m_MaxHealth = race.health;
        m_CurrentHealth = race.health;
        m_Attack = race.attack;
        m_AttackRange = race.attackRange;
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
}
