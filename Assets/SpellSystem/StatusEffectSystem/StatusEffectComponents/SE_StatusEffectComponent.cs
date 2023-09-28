using UnityEngine;

public class SE_StatusEffectTimeComponent : SE_TimeComponent
{
    private enum TargetName { Caster, Target, EventReturn }

    [SerializeField] private EntityEventName m_EntityEvent;
    [SerializeField] private TargetName m_ApplyTo;
    [SerializeField] private StatusEffect m_StatusEffect;
    //So you have copy for every entity
    private StatusEffect m_StatusEffectCopy;

    public override void Init()
    {
        m_StatusEffectCopy = ScriptableObject.Instantiate(m_StatusEffect);

        OnEffectApplied += EnableEffectApplification;
        OnEffectDeapplied += DisableEffectApplification;
        base.Init();
    }

    private void EnableEffectApplification()
    {
        if (m_ApplyTo == TargetName.EventReturn)
        {
            GameEvent<Transform> entityEvent = targetEntity.Events.GetEvent<Transform>(m_EntityEvent, false);
            if (entityEvent == null)
            {
                Debug.Log("Event named " + m_EntityEvent + " doesn't have Transform return can't use EventReturn");
            }
            else
            {
                entityEvent.Subscribe(ActivateEventReturn);
            }
            return;
        }
        targetEntity.Events.GetEvent(m_EntityEvent).Subscribe(Activate);
    }

    private void DisableEffectApplification()
    {
        targetEntity.Events.GetEvent(m_EntityEvent).Unsubscribe(Activate);
        targetEntity.Events.GetEvent<Transform>(m_EntityEvent, false)?.Unsubscribe(ActivateEventReturn);
    }
    public override void Destroy()
    {
        DisableEffectApplification();
    }

    private void Activate()
    {
        target.GetComponent<StatusEffectHandler>().AddEffect(m_StatusEffectCopy);
    }
    //TODO: Might change it from Transform to Target
    private void ActivateEventReturn(Transform target)
    {
        if (target.TryGetComponent<StatusEffectHandler>(out StatusEffectHandler handler))
        {
            handler.AddEffect(m_StatusEffectCopy);
        }
        else
        {
            Debug.Log("There is no StatusEffectHandlerComponent on EntityEvent return");
        }
    }
}