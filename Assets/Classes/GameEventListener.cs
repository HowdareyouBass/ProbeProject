using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEventListener
{
    public GameEvent gameEvent;
    public event Action onEventTriggered;

    public void OnEventTriggered()
    {
        if (onEventTriggered == null)
        {
            Debug.Log("No functions assigned to event");
            return;
        }
        onEventTriggered.Invoke();
    } 
}
