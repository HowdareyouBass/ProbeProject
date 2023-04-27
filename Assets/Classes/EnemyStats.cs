using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    public void SetType(EnemyType enemyType)
    {
        m_CurrentHealth = enemyType.health;
        m_MaxHealth = enemyType.health;
    }
}
