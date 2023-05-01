using UnityEngine;

public class Health : MonoBehaviour
{
    private GameEvent m_OnDeath;
    private GameEvent <float> m_OnDamaged;
    
    private EntityStats stats;
    Entity entity;

    void Start()
    {
        entity = gameObject.GetComponent<EntityScript>().GetEntity();
        m_OnDeath = entity.GetEvent(EventName.OnDeath);
        m_OnDamaged = entity.GetEvent<float>(EventName.OnDamaged);
        if (m_OnDeath == null) Debug.LogWarning("No on damaged event on this object", gameObject);
        if (m_OnDamaged == null) Debug.LogWarning("No on death event on this object", gameObject);
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
