using System;
using UnityEngine;

//God object problem maybe? FIXME:
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

    public void SpendStamina(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        stats.SpendStamina(amount);
        events.GetEvent<float>(EntityEventName.OnStaminaChanged, true).Trigger(amount);
    }
    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        events.GetEvent<float>(EntityEventName.OnHitTaken, true).Trigger(amount);
        if (stats.BarrierIsSet)
        {
            Debug.Log("Barrier is set");
            return;
        }
        stats.DecreaseHealth(amount);
        events.GetEvent<float>(EntityEventName.OnDamaged, true).Trigger(amount);
        events.GetEvent<float>(EntityEventName.OnHealthChanged, true).Trigger(amount);
        if (stats.CurrentHealth <= 0)
        {
            events.GetEvent(EntityEventName.OnDeath).Trigger();
        }
    }
    public void EnableBarrier()
    {
        stats.BarrierIsSet = true;
    }
    public void DisableBarrier(float barrierDamageOverpassed)
    {
        stats.BarrierIsSet = false;
        TakeDamage(barrierDamageOverpassed);
    }
    public void Regenerate()
    {
        if (stats.Regeneration <= 0)
            return;
        stats.Heal(stats.Regeneration * Time.deltaTime);
        if (stats.CurrentHealth < stats.MaxHealth)
            events.GetEvent<float>(EntityEventName.OnHealthChanged, true).Trigger(stats.Regeneration);
    }

    public void ApplyPassive(PassiveStats effectStats)
    {
        stats.AddPassiveStats(effectStats);
    }
    public void DeapplyPassive(PassiveStats effectStats)
    {
        stats.SubtractPassiveStats(effectStats);
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

    public void AttackTarget(Target target)
    {
        if (UnityEngine.Random.value >= stats.HitChance - target.TargetEntity.stats.Evasion)
        {
            Debug.Log("Miss");
            return;
        }
        target.TargetEntity.TakeDamage(stats.AttackDamage);
        events.GetEvent<Transform>(EntityEventName.OnAttack, true).Trigger(target.transform);
    }
    public virtual float GetAttackCooldown()
    {
        return stats.BaseAttackSpeed * 100 / stats.AttackSpeed;
    }
}
