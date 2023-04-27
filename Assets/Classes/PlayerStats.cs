using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : EntityStats
{
    private float m_Evasion = 0;

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
}
