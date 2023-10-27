using System;
using UnityEditor.Rendering;
using UnityEngine;

//God object problem maybe? FIXME:
public abstract class Entity
{
    public abstract EntityStats Stats { get; }
    public EntityEvents Events { get; private set; }

    public bool CanAttack { get; private set; } = true;
    public bool CanMove { get; private set; } = true;
    public bool CanCast { get; private set; } = true;
    public bool CanLook { get; private set; } = true;

    public Entity()
    {
        Events = new EntityEvents();
    }

    public void GainExperience(float value)
    {
        if (value <= 0) return;
        Stats.AddExperience(value);
    }

    public void SpendStamina(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        Stats.SpendStamina(amount);
        Events.GetEvent<float>(EntityEventName.OnStaminaChanged, true).Trigger(amount);
    }
    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        if (amount == 0) return;
        Events.GetEvent<float>(EntityEventName.OnHitTaken, true).Trigger(amount);
        if (Stats.BarrierIsSet)
        {
            Debug.Log("Barrier is set");
            return;
        }
        Stats.DecreaseHealth(amount);
        Events.GetEvent<float>(EntityEventName.OnDamaged, true).Trigger(amount);
        Events.GetEvent<float>(EntityEventName.OnHealthChanged, true).Trigger(amount);
        if (Stats.CurrentHealth <= 0)
        {
            Events.GetEvent<float>(EntityEventName.OnDeath, true).Trigger(Stats.ExperienceForKill);
        }
    }
    public void EnableBarrier()
    {
        Stats.BarrierIsSet = true;
    }
    public void DisableBarrier(float barrierDamageOverpassed)
    {
        Stats.BarrierIsSet = false;
        TakeDamage(barrierDamageOverpassed);
    }
    public void Regenerate()
    {
        if (Stats.Regeneration <= 0)
            return;
        Stats.Heal(Stats.Regeneration * Time.deltaTime);
        if (Stats.CurrentHealth < Stats.MaxHealth)
            Events.GetEvent<float>(EntityEventName.OnHealthChanged, true).Trigger(Stats.Regeneration);
    }

    public void ApplyPassive(PassiveStats effectStats)
    {
        Stats.AddPassiveStats(effectStats);
    }
    public void DeapplyPassive(PassiveStats effectStats)
    {
        Stats.SubtractPassiveStats(effectStats);
    }

    public void Sleep()
    {
        Events.GetEvent(EntityEventName.StopMovement).Trigger();
        CanMove = false;
        CanAttack = false;
        CanCast = false;
    }
    public void Awake()
    {
        CanMove = true;
        CanAttack = true;
        CanCast = true;
    }

    public void Silence()
    {
        Debug.Log("Should silence");
        CanCast = false;
    }
    public void Desilence()
    {
        Debug.Log("should desilence");
        CanCast = true;
    }

    public void AttackTarget(Target target)
    {
        if (UnityEngine.Random.value >= Stats.HitChance - target.TargetEntity.Stats.Evasion)
        {
            Debug.Log("Miss");
            return;
        }
        target.TargetEntity.TakeDamage(Stats.AttackDamage);
        Events.GetEvent<Transform>(EntityEventName.OnAttack, true).Trigger(target.transform);
    }
    public virtual float GetAttackCooldown()
    {
        return Stats.AttackSpeed / (Stats.BaseAttackSpeed * 100);
    }
}
