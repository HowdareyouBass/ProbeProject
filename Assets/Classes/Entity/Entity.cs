using System;
using UnityEngine;

public abstract class Entity
{
    public EntityStats stats { get; private set; }
    public EntityEvents events;

    public bool canAttack { get; private set; } = true;
    public bool canMove { get; private set; } = true;
    public bool canCast { get; private set; } = true;
    public bool canLook { get; private set; } = true;

    public Entity()
    {
        stats = new EntityStats();
        events = new EntityEvents();
    }

    public GameEvent<T> GetEvent<T>(EntityEventName name, bool canBeError) 
    {
        GameEvent<T> res = events.events[name] as GameEvent<T>;
        if (res == null && canBeError)
            Debug.LogWarning("Wrong type of event");
        return res; 
    }
    public GameEvent GetEvent (EntityEventName name) { return events.events[name]; }
    public virtual float GetAttackCooldown() { return stats.baseAttackSpeed * 100 / stats.attackSpeed; }

    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        stats.TakeDamage(amount);
    }
    public void Regenerate()
    {
        if (stats.regen < 0)
            return;
        stats.Heal(stats.regen * Time.deltaTime);
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        if (effect.applySleep)
        {
            GetEvent(EntityEventName.OnAttackDisabled).Trigger();
            canAttack = false;
            GetEvent(EntityEventName.OnMovementDisabled).Trigger();
            canMove = false;
            GetEvent(EntityEventName.OnCastingDisabled).Trigger();
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
