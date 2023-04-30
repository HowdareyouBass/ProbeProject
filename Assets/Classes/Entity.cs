using System;
using UnityEngine;

public abstract class Entity
{
    protected EntityStats stats { get; private set; }

    public Entity()
    {
        stats = new EntityStats();
    }

    public virtual GameEvent GetOnDeathEvent() { return null; }
    public virtual GameEvent<float> GetOnDamageEvent() { return null; }
    public virtual GameEvent GetOnAttackEvent() { return null; }
    public virtual Spell GetSpellFromSlot(int spellSlot) { return null; }

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
