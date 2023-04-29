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
        IEntity entity = gameObject.GetComponent<IEntity>();
        //Debug.Assert(entity != null);
        m_OnDeath = entity.GetOnDeathEvent();
        m_OnDamaged = entity.GetOnDamageEvent();
        stats = entity.GetStats();
        //Debug.Assert(stats != null);
    }

    public void TakeDamage(float amount)
    {
        stats.TakeDamage(amount);
        if (!m_OnDamaged.Trigger(amount))
            Debug.Log("No action assigned to event " + nameof(m_OnDamaged) + " in gameobject " + gameObject.name);


        if (stats.GetCurrentHealth() <= 0)
        {
            if (!m_OnDeath.Trigger())
                Debug.Log("No action assigned to event " + nameof(m_OnDamaged) + " in gameobject " + gameObject.name);
        }
    }
}
