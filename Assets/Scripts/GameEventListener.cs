using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener
{
    public GameEvent gameEvent;
    [System.NonSerialized] public UnityEvent onEventTriggered = new UnityEvent();

    public void OnEventTriggered()
    {
        onEventTriggered.Invoke();
    } 
}
