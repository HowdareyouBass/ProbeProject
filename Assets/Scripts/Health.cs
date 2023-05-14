using UnityEngine;

public class Health : MonoBehaviour
{
    private GameEvent m_OnDeath;
    private GameEvent<float> m_OnDamaged;
    private GameEvent<float> m_OnHealthChanged;
    private Entity m_Entity;

    private void Start()
    {
        m_Entity = gameObject.GetComponent<EntityScript>().GetEntity();

        m_OnDeath = m_Entity.events.GetEvent(EntityEventName.OnDeath);
        m_OnDamaged = m_Entity.events.GetEvent<float>(EntityEventName.OnDamaged, true);
        m_OnHealthChanged = m_Entity.events.GetEvent<float>(EntityEventName.OnHealthChanged, true);
    }

    private void FixedUpdate()
    {
        m_Entity.Regenerate();
        m_OnHealthChanged?.Trigger(m_Entity.stats.regen);
    }

    public void TakeDamage(float amount)
    {
        m_Entity.TakeDamage(amount);
        m_OnDamaged?.Trigger(amount);
        if (m_Entity.stats.currentHealth <= 0)
        {
            m_OnDeath?.Trigger();
        }
    }
}