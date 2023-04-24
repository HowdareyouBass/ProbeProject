using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    private float m_CurrentHealth = 0;
    private float m_MaxHealth = 0;
    private float m_Attack = 0;
    private float m_AttackRange = 0;
    private float m_AttackSpeed = 0;
    private float m_BaseAttackSpeed = 1;
    private float m_Evasion = 0;

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

    public void ApplyPassiveSpell(Spell spell)
    {
        //Adding passive spell stats
        PassiveSpellStats stats = spell.GetPassiveStats();
        m_AttackSpeed += stats.GetAttackSpeed();

        //Multiplying percents of passive spell stats
        Percents percents = spell.GetPercents();
        m_Attack *= 1 + percents.GetAttackPercent();
        m_MaxHealth *= 1 + percents.GetMaxHealthPercent();
    }
    
    public void ApplyStatusEffect(StatusEffect effect)
    {
        StatusEffectStats stats = effect.GetStatusEffectStats();

        m_AttackSpeed += stats.GetAttackSpeed();
    }

    public void DeapplyStatusEffect(StatusEffect effect)
    {
        StatusEffectStats stats = effect.GetStatusEffectStats();

        m_AttackSpeed -= stats.GetAttackSpeed();
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
}
