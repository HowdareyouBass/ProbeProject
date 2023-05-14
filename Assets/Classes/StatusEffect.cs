using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect", fileName = "New Status Effect")]
public class StatusEffect : ScriptableObject
{
    [SerializeField] private bool m_ApplySleep;
    [SerializeField] private float m_DurationInSeconds;
    [SerializeField] private float m_StartCount;
    [SerializeField] private float m_CountDelta;
    [SerializeField] private StatusEffectStats m_Stats;
    [SerializeField] private EntityEventName m_DecreaseCount;

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
        if (m_StartCount == 0 && m_DurationInSeconds == 0) Debug.LogWarning("Count and duration are both 0.", this);

        m_Entity = entity;

        GameEvent<float> decreaseByEvent = m_Entity.events.GetEvent<float>(m_DecreaseCount, false);
        GameEvent decreaseByDelta = null;

        if (decreaseByEvent == null)
        {
            decreaseByDelta = m_Entity.events.GetEvent(m_DecreaseCount);
            decreaseByDelta.Subscribe(DecreaseCount);
        }
        else
        {
            decreaseByEvent.Subscribe(DecreaseCount);
        }
        //TODO: Adding status effect to some array!!!
        m_Entity.ApplyStatusEffect(this);
        yield return new WaitForSeconds(m_DurationInSeconds);
        yield return WaitForCount();
        decreaseByEvent?.Unsubscribe(DecreaseCount);
        decreaseByDelta?.Unsubscribe(DecreaseCount);
        m_Entity.DeapplyStatusEffect(this);
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