using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    //If Field is serialized it can be edited by passive spells, status effects, potions, etc.

    private float m_CurrentHealth = 0;
    [SerializeField] private float m_MaxHealth = 0;
    [SerializeField] private float m_Attack = 0;
    [SerializeField] private float m_AttackRange = 0;
    [SerializeField] private float m_AttackSpeed = 0;
    private float m_BaseAttackSpeed = 1;
    [SerializeField] private float m_Evasion = 0;

    public PlayerStats()
    {
    }

    public PlayerStats(Race race)
    {
        m_MaxHealth = race.health;
        m_CurrentHealth = race.health;
        m_Attack = race.attack;
        m_AttackRange = race.attackRange;
        m_AttackSpeed = race.attackSpeed;
        m_BaseAttackSpeed = race.baseAttackSpeed;
        m_Evasion = race.evasion;
    }

    public static PlayerStats operator +(PlayerStats a, PlayerStats b)
    {
        PlayerStats result = new PlayerStats();

        result.m_CurrentHealth = a.m_CurrentHealth;
        result.m_MaxHealth += a.m_MaxHealth + b.m_MaxHealth;
        result.m_Attack += a.m_Attack + b.m_Attack;
        result.m_AttackRange += a.m_AttackRange + b.m_AttackRange;
        result.m_AttackSpeed += a.m_AttackSpeed + b.m_AttackSpeed;
        result.m_Evasion += a.m_Evasion + b.m_Evasion;

        return result;
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
        return m_BaseAttackSpeed;
    }

    public float GetAttackSpeed()
    {
        return m_AttackSpeed;
    }

    public void ApplySpellPercents(Percents percents)
    {
        m_Attack *= 1 + percents.GetAttackPercent();
        m_MaxHealth *= 1 + percents.GetMaxHealthPercent();
    }
}
