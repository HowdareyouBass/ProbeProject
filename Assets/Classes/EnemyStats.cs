using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    float m_CurrentHealth;
    float m_MaxHealth;
    public void SetType(EnemyType enemyType)
    {
        m_CurrentHealth = enemyType.health;
        m_MaxHealth = enemyType.health;
    }

    public void Damage(float amount)
    {
        m_CurrentHealth -= amount;
        if (m_CurrentHealth < 0)
            m_CurrentHealth = 0;
    }

    public float GetCurrentHealth()
    {
        return m_CurrentHealth / m_MaxHealth;
    }
}
