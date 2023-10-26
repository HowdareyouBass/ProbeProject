using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        m_OnHealthChanged = m_Entity.Events.GetEvent<float>(EntityEventName.OnHealthChanged, true);
        m_OnDamaged = m_Entity.Events.GetEvent<float>(EntityEventName.OnDamaged, true);

        m_OnHealthChanged?.Subscribe(SetHealthbarValue);
        m_OnDamaged?.Subscribe(SpawnEffect);
    }
    private void OnDisable()
    {
        m_OnHealthChanged?.Unsubscribe(SetHealthbarValue);
        m_OnDamaged?.Unsubscribe(SpawnEffect);
    }

    public void SetHealth(float health)
    {
        float currentHealth = m_Entity.Stats.CurrentHealth;
        float maxHealth = m_Entity.Stats.MaxHealth;
        m_HealthRenderer.material.SetFloat("_Health", currentHealth / maxHealth);
    }

    private void SpawnEffect(float effectHealthValue)
    {
        GameObject effect = Instantiate(m_DamageEffect, transform.parent);
        effect.GetComponent<TextMeshPro>().text = effectHealthValue.ToString();
    }
}