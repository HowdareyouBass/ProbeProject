using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatusEffect
{
    [SerializeField] private float m_DurationInSeconds;
    [SerializeField] private int m_StartCount;
    [SerializeField] private int m_CountDelta;
    [SerializeField] private StatusEffectStats m_Stats;
    //[SerializeField] private UnityEvent m_CountDecrease;
    [SerializeField] private GameEvent m_DecreaseCount;
    private int m_CurrentCount;

    public IEnumerator StartEffect(IEntity entity)
    {
        if (m_DecreaseCount != null)
        {
            m_DecreaseCount.Subscribe(DecreaseCount);
        }

        entity.ApplyStatusEffect(this);

        if (m_DurationInSeconds != 0)
        {
            yield return new WaitForSeconds(m_DurationInSeconds);
            entity.DeapplyStatusEffect(this);
        }
        if (m_StartCount != 0)
        {
            m_CurrentCount = m_StartCount;
            while (true)
            {
                if (m_CurrentCount <= 0)
                {
                    entity.DeapplyStatusEffect(this);
                    m_DecreaseCount.Unsubscribe(DecreaseCount);
                    yield break;
                }
                else
                {
                    yield return null;
                }
            }
        }
        yield break;
    }

    private void DecreaseCount()
    {
        Debug.Log("Decreased count");
        m_CurrentCount -= m_CountDelta;
    }

    public StatusEffectStats GetStatusEffectStats() { return m_Stats; }
}
