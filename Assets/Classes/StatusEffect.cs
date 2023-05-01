using System.Collections;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    [SerializeField] private float m_DurationInSeconds;
    [SerializeField] private float m_StartCount;
    [SerializeField] private float m_CountDelta;
    [SerializeField] private StatusEffectStats m_Stats;
    [SerializeField] private EventName m_DecreaseCount;
    private float m_CurrentCount;
    private Entity m_Entity;

    public IEnumerator StartEffect(Entity entity)
    {
        m_Entity = entity;

        GameEvent<float> decreaseByEvent = entity.GetEvent<float>(m_DecreaseCount);
        GameEvent decreaseByDelta = null;
        if (decreaseByEvent == null)
        {
            decreaseByDelta = entity.GetEvent(m_DecreaseCount);
            decreaseByDelta.Subscribe(DecreaseCount);
        }
        else
        {
            decreaseByEvent.Subscribe(DecreaseCount);
        }
        
        entity.ApplyStatusEffect(this);
        yield return WaitDuration();
        yield return WaitCount();
        decreaseByEvent?.Unsubscribe(DecreaseCount);
        decreaseByDelta?.Unsubscribe(DecreaseCount);
        yield break;
    }
    private IEnumerator WaitDuration()
    {
        if (m_DurationInSeconds != 0)
        {
            yield return new WaitForSeconds(m_DurationInSeconds);
            m_Entity.DeapplyStatusEffect(this);
        }
        yield break;
    }
    private IEnumerator WaitCount()
    {
        if (m_StartCount != 0)
        {
            m_CurrentCount = m_StartCount;
            yield return WaitForCount();
            m_Entity.DeapplyStatusEffect(this);
        }
        yield break;
    }
    private IEnumerator WaitForCount()
    {
        while (m_CurrentCount > 0)
        {
            yield return null;
        }
        yield break;
    }

    private void DecreaseCount()
    {
        Debug.Log("Decreased count with delta");
        m_CurrentCount -= m_CountDelta;
        Debug.Log(m_CurrentCount);
    }
    private void DecreaseCount(float amount)
    {
        Debug.Log("Decreased count");
        m_CurrentCount -= amount;
        Debug.Log(m_CurrentCount);
    }

    public StatusEffectStats GetStatusEffectStats() { return m_Stats; }
}
