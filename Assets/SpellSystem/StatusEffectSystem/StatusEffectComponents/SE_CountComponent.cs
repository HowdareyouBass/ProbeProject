using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class SE_CountComponent : StatusEffectComponent
{
    private enum CountDecreaseSource { CountDelta, EventReturn }

    [SerializeField] private float m_MinimumCount = 0;
    [SerializeField] private float m_MaximumCount = float.MaxValue;
    [SerializeField] private float m_StartCount;
    [SerializeField] private EntityEventName m_EntityEvent;
    [SerializeField] private CountDecreaseSource m_DecreaseBy;
    [SerializeField] private float m_CountDelta;

    [SerializeField] private bool m_ReverseEventReturnValue;

    public float CurrentCount { get; private set; }
    
    public event Action OnEffectApplied;
    public event Action<float> OnEffectDeapplied;

    public override void Init()
    {
        CurrentCount = m_StartCount;
        if (m_DecreaseBy == CountDecreaseSource.EventReturn)
        {
            targetEntity.events.GetEvent<int>(m_EntityEvent, true)?.Subscribe(ChangeCountByEvent);
            targetEntity.events.GetEvent<float>(m_EntityEvent, true)?.Subscribe(ChangeCountByEvent);
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
        targetEntity.events.GetEvent<float>(m_EntityEvent, false)?.Unsubscribe(ChangeCountByEvent);
    }

    private async Task StartCount()
    {
        OnEffectApplied?.Invoke();
        while (!statusEffect.stopEffectToken.IsCancellationRequested && CurrentCount > m_MinimumCount && CurrentCount < m_MaximumCount)
        {
            await Task.Yield();
        }
        OnEffectDeapplied?.Invoke(-CurrentCount);
    }
    private void ChangeCountByEvent(int amount)
    {
        if (m_ReverseEventReturnValue)
        {
            amount = -amount;
        }
        CurrentCount += amount;
    }
    private void ChangeCountByEvent(float amount)
    {
        if (m_ReverseEventReturnValue)
        {
            amount = -amount;
        }
        CurrentCount += amount;
    }
    private void ChangeCountByDelta()
    {
        Debug.Log(CurrentCount);
        CurrentCount += m_CountDelta;
    }
}