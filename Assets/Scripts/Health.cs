using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameEvent m_OnDeath;
    [SerializeField] private GameEvent m_OnDamaged;
    
    private PlayerStats stats;

    void Start()
    {
        //Could not use floats because they just copy instead of referencing float from stats
        stats = Player.playerStats;
    }

    public void Damage(float amount)
    {
        stats.currentHealth -= amount;
        if (m_OnDamaged != null)
            m_OnDamaged.TriggerEvent();
        if (stats.currentHealth <= 0)
        {
            if (m_OnDeath != null)
                m_OnDeath.TriggerEvent();
        }
    }
}
