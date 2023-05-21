using System.Collections;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    private enum DecreaseCountBy { None, EventReturnFloat, CountDelta }

    [SerializeField] private bool m_ApplySleep;
    [SerializeField] private float m_DurationInSeconds;
    [Header("Count")]
    [SerializeField] private float m_StartCount;
    [SerializeField] private float m_CountDelta;
    [SerializeField] private DecreaseCountBy m_DecreaseCountBy;
    [SerializeField] private EntityEventName m_DecreaseCount;
    [Header("Tick")]
    [SerializeField] private float m_TickTimeInSeconds;
    [SerializeField] private float m_TickDamage;
    [SerializeField] private StatusEffectStats m_Stats;

    public bool applySleep { get => m_ApplySleep; }
    public StatusEffectStats stats { get => m_Stats; }

    private float m_CurrentCount;
    private Entity m_Entity;
    private float m_Time;

    private bool m_IsActive => m_CurrentCount > 0 || m_Time > 0;

    public void Start()
    {
        m_CurrentCount = m_StartCount;
        m_Time = m_DurationInSeconds;
    }

    public IEnumerator StartEffectRoutine(Entity entity)
    {
        m_CurrentCount = m_StartCount;
        m_Entity = entity;
        if (m_StartCount == 0 && m_DurationInSeconds == 0) Debug.LogWarning("Count and duration are both 0.");


        GameEvent<float> decreaseByFloat = null;
        GameEvent decreaseByDelta = null;

        if(m_DecreaseCountBy == DecreaseCountBy.EventReturnFloat)
        {
            decreaseByFloat = m_Entity.events.GetEvent<float>(m_DecreaseCount, true);
            decreaseByFloat.Subscribe(DecreaseCount);
        }
        if(m_DecreaseCountBy == DecreaseCountBy.CountDelta)
        {
            decreaseByDelta = m_Entity.events.GetEvent(m_DecreaseCount);
            decreaseByDelta.Subscribe(DecreaseCount);
        }

        m_Entity.ApplyStatusEffect(this);
        yield return WaitForTime();
        decreaseByFloat?.Unsubscribe(DecreaseCount);
        decreaseByDelta?.Unsubscribe(DecreaseCount);
        m_Entity.DeapplyStatusEffect(this);
        yield break;
    }
    private IEnumerator WaitForTime()
    {
        float timer = 0;
        float tickTimer = 0;

        while (timer < m_DurationInSeconds)
        {
            if (m_CurrentCount <= 0 && m_StartCount != 0)
            {
                yield break;
            }
            timer += Time.deltaTime;
            tickTimer += Time.deltaTime;
            if (tickTimer >= m_TickTimeInSeconds && m_TickTimeInSeconds != 0)
            {
                tickTimer = 0;
                m_Entity.TakeDamage(m_TickDamage);
            }
            yield return null;
        }
        yield break;
    }

    private void DecreaseCount()
    {
        m_CurrentCount -= m_CountDelta;
        Debug.Log(m_CurrentCount);
    }
    private void DecreaseCount(float amount)
    {
        Debug.Log("Decreased count");
        m_CurrentCount -= amount;
        Debug.Log(m_CurrentCount);
    }
}