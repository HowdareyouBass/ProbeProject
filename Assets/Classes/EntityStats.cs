using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats
{
    protected float m_CurrentHealth = 0;
    protected float m_MaxHealth = 0;
    protected float m_AttackSpeed = 20;
    protected float m_AttackDamage = 0;
    protected float m_AttackRange = 10;
    protected float m_BaseAttackSpeed = 1;

    public void Damage(float amount)
    {
        m_CurrentHealth -= amount;
        if (m_CurrentHealth < 0)
        {
            m_CurrentHealth = 0;
        }
    }

    public void ApplyPassiveSpell(Spell spell)
    {
        //Adding passive spell stats
        PassiveSpellStats stats = spell.GetPassiveStats();
        m_AttackSpeed += stats.GetAttackSpeed();

        //Multiplying percents of passive spell stats
        Percents percents = spell.GetPercents();
        m_AttackDamage *= 1 + percents.GetAttackPercent();
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

    public float GetCurrentHealth() { return m_CurrentHealth; }
    public float GetAttackDamage() { return m_AttackDamage; }
    public float GetAttackSpeed() { return m_AttackSpeed; }
    public float GetBaseAttackSpeed() { return m_BaseAttackSpeed; }
    public float GetAttackRange() { return m_AttackRange; }
    public float GetMaxHealth() { return m_MaxHealth; }
}
