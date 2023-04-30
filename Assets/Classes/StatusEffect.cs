using System.Collections;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    [SerializeField] private float m_DurationInSeconds;
    [SerializeField] private float m_StartCount;
    [SerializeField] private float m_CountDelta;
    [SerializeField] private StatusEffectStats m_Stats;
    [SerializeField] private EventName m_DecreaseCountEventName;
    private float m_CurrentCount;

    public IEnumerator StartEffect(Entity entity)
    {
        GameEvent decrease = entity.GetEvents()[m_DecreaseCountEventName];
        GameEvent<float> decreaseFloat = decrease as GameEvent<float>;
        if (decreaseFloat == null)
            decrease.Subscribe(DecreaseCount);
        else
            decreaseFloat.Subscribe(DecreaseCount);
            
        entity.ApplyStatusEffect(this);

        if (m_DurationInSeconds != 0)
        {
            yield return new WaitForSeconds(m_DurationInSeconds);
            entity.DeapplyStatusEffect(this);
        }
        if (m_StartCount != 0)
        {
            m_CurrentCount = m_StartCount;
            yield return WaitForCount();
            entity.DeapplyStatusEffect(this);
            decrease.Unsubscribe(DecreaseCount);
            yield break;
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
