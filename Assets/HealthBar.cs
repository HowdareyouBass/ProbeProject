using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Renderer healthRenderer;
    private IEntity entity;

    void Start()
    {
        entity = transform.root.GetComponent<IEntity>();
        entity.GetOnDamageEvent().Subscribe(DecreaseHealthValue);
    }

    private void DecreaseHealthValue(float amount)
    {
        float currentHealth = entity.GetCurrentHealth();
        float maxHealth = entity.GetMaxHealth();
        healthRenderer.material.SetFloat("_Health", (currentHealth - amount) / maxHealth);
    }
}
