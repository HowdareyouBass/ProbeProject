using System;
using UnityEngine;

public abstract class Entity
{
    public EntityStats stats { get; private set; }
    public EntityEvents events { get; private set; }

    public bool canAttack { get; private set; } = true;
    public bool canMove { get; private set; } = true;
    public bool canCast { get; private set; } = true;
    public bool canLook { get; private set; } = true;

    public Entity()
    {
        stats = new EntityStats();
        events = new EntityEvents();
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        stats.TakeDamage(amount);
        events.GetEvent<float>(EntityEventName.OnDamaged, true).Trigger(amount);
        if (stats.currentHealth <= 0)
        {
            events.GetEvent(EntityEventName.OnDeath).Trigger();
        }
    }
    public void Regenerate()
    {
        if (stats.regen <= 0)
            return;
        stats.Heal(stats.regen * Time.deltaTime);
        if (stats.currentHealth < stats.maxHealth)
            events.GetEvent<float>(EntityEventName.OnHealthChanged, true).Trigger(stats.regen);
    }

    public void ApplyPassive(PassiveStats effectStats)
    {
        stats.ApplyStatusEffect(effectStats);
    }
    public void DeapplyPassive(PassiveStats effectStats)
    {
        stats.DeapplyStatusEffect(effectStats);
    }

    public void Sleep()
    {
        events.GetEvent(EntityEventName.StopMovement).Trigger();
        canMove = false;
        canAttack = false;
        canCast = false;
    }
    public void Awake()
    {
        canMove = true;
        canAttack = true;
        canCast = true;
    }

    public void Silence()
    {
        Debug.Log("Should silence");
        canCast = false;
    }
    public void Desilence()
    {
        Debug.Log("should desilence");
        canCast = true;
    }

    public void DamageTarget(Transform target)
    {
        target.GetComponent<Health>().TakeDamage(stats.attackDamage);
        events.GetEvent<Transform>(EntityEventName.OnAttack, true).Trigger(target);
    }
    public virtual float GetAttackCooldown()
    {
        return stats.baseAttackSpeed * 100 / stats.attackSpeed;
    }
}
