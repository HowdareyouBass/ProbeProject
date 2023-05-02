using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    protected EntityStats stats { get; private set; }
    public EntityEvents events;

    public Entity()
    {
        stats = new EntityStats();
        events = new EntityEvents();
    }

    public virtual Spell1 GetSpellFromSlot(int spellSlot) { return null; }

    public GameEvent<T> GetEvent<T>(EventName name) 
    {
        GameEvent<T> res = events.events[name] as GameEvent<T>;
        if (res == null)
            Debug.LogWarning("Wrong type of event");
        return res; 
    }
    public GameEvent GetEvent (EventName name) { return events.events[name]; }

    public float GetAttackRange() { return stats.GetAttackRange(); }
    public float GetAttackDamage() { return stats.GetAttackDamage(); }
    public virtual float GetAttackCooldown() { return stats.GetBaseAttackSpeed() * 100 / stats.GetAttackSpeed(); }
    public float GetMaxHealth() { return stats.GetMaxHealth(); }
    public float GetCurrentHealth() { return stats.GetCurrentHealth(); }

    public virtual void ApplyAllPassiveSpells() {}
    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        stats.TakeDamage(amount);
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        stats.ApplyStatusEffect(effect);
    }
    public void DeapplyStatusEffect(StatusEffect effect)
    {
        stats.DeapplyStatusEffect(effect);
    }
    public void DamageTarget(RaycastHit target)
    {
        target.transform.GetComponent<Health>().TakeDamage(stats.GetAttackDamage());
    }
}
