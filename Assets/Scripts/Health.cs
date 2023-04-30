using UnityEngine;

public class Health : MonoBehaviour
{
    private GameEvent m_OnDeath;
    private GameEvent<float> m_OnDamaged;
    
    private EntityStats stats;
    Entity entity;

    void Start()
    {
        entity = gameObject.GetComponent<EntityScript>().GetEntity();
        //Debug.Assert(entity != null);
        m_OnDeath = entity.GetOnDeathEvent();
        m_OnDamaged = entity.GetOnDamageEvent();
        if (m_OnDeath == null) Debug.LogWarning("No on damaged event on this object", gameObject);
        if (m_OnDamaged == null) Debug.LogWarning("No on death event on this object", gameObject);
        //Debug.Assert(stats != null);
    }

    public void TakeDamage(float amount)
    {
        entity.TakeDamage(amount);
        m_OnDamaged?.Trigger(amount);

        if (entity.GetCurrentHealth() <= 0)
        {
            m_OnDeath?.Trigger();
        }
    }
}
