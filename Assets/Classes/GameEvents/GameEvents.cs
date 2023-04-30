using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum EventName
{
    None,
    OnDeath,
    OnAttack,
    OnDamaged,
}

public class EntityEvents
{
    //Creating array of all events that are inside enum
    private static int m_MaxEventsNumber = Enum.GetValues(typeof(EventName)).Cast<int>().Max();
    public Dictionary<EventName,GameEvent> events;
    public EntityEvents()
    {
        events = new Dictionary<EventName, GameEvent>(m_MaxEventsNumber);
        events.Add(EventName.None, new GameEvent());
        events.Add(EventName.OnDeath, new GameEvent());
        events.Add(EventName.OnAttack, new GameEvent());
        events.Add(EventName.OnDamaged, new GameEvent<float>());
    }
}