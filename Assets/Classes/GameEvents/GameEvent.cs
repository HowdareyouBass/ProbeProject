using System;

public class GameEvent
{
    private Action m_Action;

    public void Trigger()
    {
        m_Action?.Invoke();
    }

    public void Subscribe(Action function)
    {
        m_Action += function;
    }

    public void Unsubscribe(Action function)
    {
        m_Action -= function;
    }
}

public class GameEvent<T> : GameEvent
{
    private Action<T> m_Action;

    public void Trigger(T value)
    {
        base.Trigger();
        m_Action?.Invoke(value);
    }

    public void Subscribe(Action<T> function)
    {
        m_Action += function;
    }

    public void Unsubscribe(Action<T> function)
    {
        m_Action -= function;
    }
}