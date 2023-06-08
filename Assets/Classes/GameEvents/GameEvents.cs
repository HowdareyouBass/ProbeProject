using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Events<T> where T : Enum
{
    protected static int m_MaxEventsNumber { get; private set; } = Enum.GetValues(typeof(T)).Cast<int>().Max();
    private Dictionary<T,GameEvent> m_Events;

    public Events()
    {
        m_Events = new Dictionary<T, GameEvent>();
        InitEvents();
    }
    protected abstract void InitEvents();

    public GameEvent GetEvent(T name)
    {
        return m_Events[name];
    }
    //                                                      False if you handle null value by yourself
    public GameEvent<EventType> GetEvent<EventType>(T name, bool canBeError)
    {
        GameEvent<EventType> res = m_Events[name] as GameEvent<EventType>;
        if (res == null && canBeError)
            Debug.LogWarning("Wrong type of event");
        return res;
    }

    protected void AddEvent<EventType>(T name)
    {
        m_Events.Add(name, new GameEvent<EventType>());
    }
    protected void AddEvent(T name)
    {
        m_Events.Add(name, new GameEvent());
    }
}

public enum EntityEventName
{
    None,
    OnDeath,
    OnAttack,
    OnDamaged,
    OnHealthChanged,
    StopMovement
}
public class EntityEvents : Events<EntityEventName>
{
    protected override void InitEvents()
    {
        AddEvent(EntityEventName.None);
        AddEvent(EntityEventName.OnDeath);
        AddEvent<Transform>(EntityEventName.OnAttack);
        AddEvent<float>(EntityEventName.OnDamaged);
        AddEvent<float>(EntityEventName.OnHealthChanged);
        AddEvent(EntityEventName.StopMovement);
    }
}

public enum SpellEventName
{
    None,
    OnImpact,
    OnCast,
    OnTryCast
}
public class SpellEvents : Events<SpellEventName>
{
    protected override void InitEvents()
    {
        AddEvent(SpellEventName.None);
        AddEvent(SpellEventName.OnImpact);
        AddEvent(SpellEventName.OnCast);
        AddEvent(SpellEventName.OnTryCast);
    }
}