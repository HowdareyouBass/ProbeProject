using System;

public class GameEvent<T>
{
    private event Action<T> action;

    public void Trigger(T value)
    {
        action?.Invoke(value);
    }

    public void Subscribe(Action<T> function)
    {
        action += function;
    }

    public void Unsubscribe(Action<T> function)
    {
        action -= function;
    }
}

public class GameEvent
{
    private Action action;

    public void Trigger()
    {
        action?.Invoke();
    }

    public void Subscribe(Action function)
    {
        action += function;
    }

    public void Unsubscribe(Action function)
    {
        action -= function;
    }
}
