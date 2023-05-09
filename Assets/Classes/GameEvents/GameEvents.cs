using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Events<T> where T : Enum
{
    protected static int m_MaxEventsNumber { get; private set; } = Enum.GetValues(typeof(T)).Cast<int>().Max();
    public Dictionary<T,GameEvent> events { get; protected set; }

    public Events()
    {
        events = new Dictionary<T, GameEvent>();
        InitEvents();
    }
    protected abstract void InitEvents();
}

public enum EntityEventName
{
    None,
    OnDeath,
    OnAttack,
    OnDamaged,
    OnHealthChanged,
    OnAttackDisabled,
    OnMovementDisabled,
    OnCastingDisabled,
}
public class EntityEvents : Events<EntityEventName>
{
    protected override void InitEvents()
    {
        events.Add(EntityEventName.None, new GameEvent());
        events.Add(EntityEventName.OnDeath, new GameEvent());
        events.Add(EntityEventName.OnAttack, new GameEvent<Transform>());
        events.Add(EntityEventName.OnDamaged, new GameEvent<float>());
        events.Add(EntityEventName.OnHealthChanged, new GameEvent<float>());
        events.Add(EntityEventName.OnAttackDisabled, new GameEvent());
        events.Add(EntityEventName.OnMovementDisabled, new GameEvent());
        events.Add(EntityEventName.OnCastingDisabled, new GameEvent());
    }
}

public enum SpellEventName
{
    None,
    OnImpact,
    OnCast
}
public class SpellEvents : Events<SpellEventName>
{
    protected override void InitEvents()
    {
        events.Add(SpellEventName.None, new GameEvent());
        events.Add(SpellEventName.OnImpact, new GameEvent());
        events.Add(SpellEventName.OnCast, new GameEvent());
    }
}