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
    [SerializeField] private GameEventListener m_DecreaseCount = new GameEventListener();

    private int m_CurrentCount;

    public IEnumerator StartEffect(Player player)
    {
        if (m_DecreaseCount.gameEvent != null)
        {
            //Adding function to Action wich will be called when event triggers
            m_DecreaseCount.onEventTriggered += DecreaseCount;
        }
        //Adding this listener in count when the event triggers
        m_DecreaseCount.gameEvent.AddListener(m_DecreaseCount);

        player.ApplyStatusEffect(this);

        if (m_DurationInSeconds != 0)
        {
            yield return new WaitForSeconds(m_DurationInSeconds);
            player.DeapplyStatusEffect(this);
        }
        if (m_StartCount != 0)
        {
            m_CurrentCount = m_StartCount;
            while (true)
            {
                if (m_CurrentCount <= 0)
                {
                    player.DeapplyStatusEffect(this);
                    m_DecreaseCount.gameEvent.RemoveListener(m_DecreaseCount);
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
