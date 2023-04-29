using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public sealed class GameEvent<T>
{
    private event Action<T> action;

    public bool Trigger(T value)
    {
        if (action != null)
        {
            action.Invoke(value);
            return true;
        }
        return false;
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

    //Returns false if action is not assigned
    public bool Trigger()
    {
        if (action != null)
        {
            action.Invoke();
            return true;
        }
        return false;
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
