using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class SE_CountComponent : StatusEffectComponent
{
    private enum CountDecreaseSource { CountDelta, EventReturn }

    [SerializeField] private int m_StartCount;
    [SerializeField] private EntityEventName m_EntityEvent;
    [SerializeField] private CountDecreaseSource m_DecreaseBy;
    [SerializeField] private int m_CountDelta;

    private int m_CurrentCount;
    
    public event Action OnEffectApplied;
    public event Action OnEffectDeapplied;

    public override void Init()
    {
        m_CurrentCount = m_StartCount;
        if (m_DecreaseBy == CountDecreaseSource.EventReturn)
        {
            targetEntity.events.GetEvent<int>(m_EntityEvent, true)?.Subscribe(ChangeCountByEvent);
        }
        else
        {
            targetEntity.events.GetEvent(m_EntityEvent).Subscribe(ChangeCountByDelta);
        }
        statusEffect.effectTasks.Add(StartCount());
    }

    public override void Destroy()
    {
        targetEntity.events.GetEvent(m_EntityEvent).Unsubscribe(ChangeCountByDelta);
        targetEntity.events.GetEvent<int>(m_EntityEvent, false)?.Unsubscribe(ChangeCountByEvent);
    }

    private async Task StartCount()
    {
        OnEffectApplied?.Invoke();
        while (!statusEffect.stopEffectToken.IsCancellationRequested && m_CurrentCount > 0)
        {
            await Task.Yield();
        }
        OnEffectDeapplied?.Invoke();
    }
    private void ChangeCountByEvent(int amount)
    {
        m_CurrentCount += amount;
    }
    private void ChangeCountByDelta()
    {
        Debug.Log(m_CurrentCount);
        m_CurrentCount += m_CountDelta;
    }
}