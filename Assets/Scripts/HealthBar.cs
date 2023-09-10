using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Renderer m_HealthRenderer;
    [SerializeField] private GameObject m_DamageEffect;
    private Entity m_Entity;
    private GameEvent<float> m_OnHealthChanged;
    private GameEvent<float> m_OnDamaged;

    private void OnEnable()
    {
        m_Entity = transform.root.GetComponent<EntityScript>().GetEntity();

        m_OnHealthChanged = m_Entity.events.GetEvent<float>(EntityEventName.OnHealthChanged, true);
        m_OnDamaged = m_Entity.events.GetEvent<float>(EntityEventName.OnDamaged, true);

        m_OnHealthChanged?.Subscribe(SetHealthbarValue);
        m_OnDamaged?.Subscribe(SpawnEffect);
    }
    private void OnDisable()
    {
        m_OnHealthChanged?.Unsubscribe(SetHealthbarValue);
        m_OnDamaged?.Unsubscribe(SpawnEffect);
    }

    private void SetHealthbarValue(float amount)
    {
        float currentHealth = m_Entity.stats.CurrentHealth;
        float maxHealth = m_Entity.stats.MaxHealth;
        m_HealthRenderer.material.SetFloat("_Health", currentHealth / maxHealth);
    }

    private void SpawnEffect(float effectHealthValue)
    {
        GameObject effect = Instantiate(m_DamageEffect, transform.parent);
        effect.GetComponent<TextMeshPro>().text = effectHealthValue.ToString();
    }
}