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
    //[SerializeField] private UnityEvent m_CountDecrease;
    private float m_CurrentCount;

    public IEnumerator StartEffect(Entity entity)
    {
        //Debug.Log(entity.GetEvents()[m_DecreaseCountEventName].GetType());
        GameEvent<float> go = entity.GetEvents()[m_DecreaseCountEventName] as GameEvent<float>;

        if (go != null)
        {
            go.Subscribe(DecreaseCount);
        }
        else
        {
            GameEvent g = entity.GetEvents()[m_DecreaseCountEventName];
            g.Subscribe(DecreaseCountWithDelta);
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
            yield return WaitForCount();
            entity.DeapplyStatusEffect(this);
            //entity.events.GetEvent(m_DecreaseCountEventName).Unsubscribe(DecreaseCount);
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

    private void DecreaseCountWithDelta()
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
