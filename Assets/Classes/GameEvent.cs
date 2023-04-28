using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public sealed class GameEvent<T>
{
    private event Action<T> action;

    public void Trigger(T value)
    {
        action.Invoke(value);
    }
    public void Subscribe(Action<T> func)
    {
        action += func;
    }
    public void Unsubscribe(Action<T> func)
    {
        action -= func;
    }
}
public sealed class GameEvent
{
    private event Action action;

    public void Trigger()
    {
        if (action != null)
        {
            action.Invoke();
            return;
        }
        Debug.LogWarning("No action assigned to this event");
    }
    public void Subscribe(Action func)
    {
        action += func;
    }
    public void Unsubscribe(Action func)
    {
        action -= func;
    }
}
