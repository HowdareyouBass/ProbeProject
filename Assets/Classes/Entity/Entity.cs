using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    //TODO: Разбить на интерфейсы для каждого из скриптов действий
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

    public virtual float GetAttackCooldown() { return stats.baseAttackSpeed * 100 / stats.attackSpeed; }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        stats.TakeDamage(amount);
    }
    public void Regenerate()
    {
        if (stats.regen <= 0)
            return;
        stats.Heal(stats.regen * Time.deltaTime);
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        if (effect.applySleep)
        {
            events.GetEvent(EntityEventName.OnAttackDisabled).Trigger();
            canAttack = false;
            events.GetEvent(EntityEventName.OnMovementDisabled).Trigger();
            canMove = false;
            events.GetEvent(EntityEventName.OnCastingDisabled).Trigger();
            canCast = false;
        }
        stats.ApplyStatusEffect(effect);
    }
    public void DeapplyStatusEffect(StatusEffect effect)
    {
        if (effect.applySleep)
        {
            canAttack = true;
            canMove = true;
            canCast = true;
        }
        stats.DeapplyStatusEffect(effect);
    }
    public void DamageTarget(Transform target)
    {
        target.GetComponent<Health>().TakeDamage(stats.attackDamage);
    }
}
