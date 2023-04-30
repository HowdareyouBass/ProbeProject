using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats
{
    //So i've found this post on stack overflow (https://stackoverflow.com/questions/3182653/are-protected-members-fields-really-that-bad) 
    //I have PlayerStats and EnemyStats i don't yet know do they deffer or they don't
    //Not to repeat the code i did this monstrocity
    //Please if you see this and you're good at programming
    //Tell me how to design that so i don't need to do this
    protected float m_CurrentHealth { get; private set; } = 0;
    protected float m_MaxHealth { get; private set; } = 0;
    protected float m_AttackSpeed { get; private set; } = 20;
    protected float m_AttackDamage { get; private set; } = 0;
    protected float m_AttackRange { get; private set; } = 10;
    protected float m_BaseAttackSpeed { get; private set; } = 1;
    protected float m_Evasion { get; private set; } = 0;

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        if (m_CurrentHealth < 0)
        {
            m_CurrentHealth = 0;
        }
    }
    public void ApplyRace(Race race)
    {
        m_CurrentHealth = race.health;
        m_MaxHealth = race.health;
        m_AttackDamage = race.attack;
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
