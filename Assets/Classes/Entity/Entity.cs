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
        events.GetEvent<float>(EntityEventName.OnHealthChanged, true).Trigger(stats.regen);
    }
    //TODO: Nullify comment
    public void ApplyPassive(StatusEffectStats effectStats)
    {
        stats.ApplyStatusEffect(effectStats);
    }
    public void DeapplyPassive(StatusEffectStats effectStats)
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
    public void DamageTarget(Transform target)
    {
        events.GetEvent<Transform>(EntityEventName.OnAttack, true).Trigger(target);
        target.GetComponent<Health>().TakeDamage(stats.attackDamage);
    }
    public virtual float GetAttackCooldown()
    {
        return stats.baseAttackSpeed * 100 / stats.attackSpeed;
    }
}
