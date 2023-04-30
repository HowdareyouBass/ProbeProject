using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    [SerializeField] private float m_DurationInSeconds;
    [SerializeField] private float m_StartCount;
    [SerializeField] private StatusEffectStats m_Stats;
    //[SerializeField] private UnityEvent m_CountDecrease;
    private Action<float> m_DecreaseCount;
    private float m_CurrentCount;

    public IEnumerator StartEffect(Entity entity)
    {
        if (m_DecreaseCount != null)
        {
            m_DecreaseCount += DecreaseCount;
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
            m_DecreaseCount -= DecreaseCount;
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

    private void DecreaseCount(float amount)
    {
        Debug.Log("Decreased count");
        m_CurrentCount -= amount;
    }

    public StatusEffectStats GetStatusEffectStats() { return m_Stats; }
}
