using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Renderer healthRenderer;
    private Entity entity;
    private GameEvent<float> OnDamaged;

    private void OnEnable()
    {
        entity = transform.root.GetComponent<EntityScript>().GetEntity();
        OnDamaged = entity.GetEvents()[EventName.OnDamaged] as GameEvent<float>;
        OnDamaged?.Subscribe(DecreaseHealthValue);
    }
    private void OnDisable()
    {
        OnDamaged?.Unsubscribe(DecreaseHealthValue);
    }
    private void DecreaseHealthValue(float amount)
    {
        float currentHealth = entity.GetCurrentHealth();
        float maxHealth = entity.GetMaxHealth();
        healthRenderer.material.SetFloat("_Health", currentHealth / maxHealth);
    }
}
