using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private GameEvent m_OnDeath;
    private GameEvent<float> m_OnDamaged;
    
    private EntityStats stats;

    void Start()
    {
        //Could not use floats because they just copy instead of referencing float from stats
        IEntity entity = GetComponent<IEntity>();
        m_OnDeath = entity.GetOnDeathEvent();
        m_OnDamaged = entity.GetOnDamageEvent();
        stats = entity.GetStats();
    }

    public void Damage(float amount)
    {
        stats.TakeDamage(amount);
        m_OnDamaged.Trigger(amount);
        if (stats.GetCurrentHealth() <= 0)
        {
            m_OnDeath.Trigger();
        }
    }
}
