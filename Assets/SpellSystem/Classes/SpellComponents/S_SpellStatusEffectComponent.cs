using UnityEngine;

public class S_StatusEffectComponent : SpellComponent
{
    private enum TargetName { Caster, Target, EventReturn }

    [SerializeField] private SpellEventName m_SpellEvent;
    [SerializeField] private EntityEventName m_EntityEvent;
    [SerializeField] private TargetName m_ApplyTo;
    [SerializeField] private StatusEffect m_StatusEffect;
    //So you have copy for every entity
    private StatusEffect m_StatusEffectCopy;

    public override void Init()
    {
        m_StatusEffectCopy = ScriptableObject.Instantiate(m_StatusEffect);
        Enable();
    }
    public override void Enable()
    {
        if (m_ApplyTo == TargetName.EventReturn)
        {
            GameEvent<Transform> entityEvent = casterEntity.events.GetEvent<Transform>(m_EntityEvent, false);
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
        spell.events.GetEvent(m_SpellEvent).Subscribe(Activate);
        casterEntity.events.GetEvent(m_EntityEvent).Subscribe(Activate);
    }
    public override void Disable()
    {
        spell.events.GetEvent(m_SpellEvent)?.Unsubscribe(Activate);
        casterEntity.events.GetEvent(m_EntityEvent).Unsubscribe(Activate);
        casterEntity.events.GetEvent<Transform>(m_EntityEvent, false)?.Unsubscribe(ActivateEventReturn);
    }

    private void Activate()
    {
        if (GetStatusEffectHandlerComponent(out StatusEffectHandler handler))
        {
            handler.AddEffect(m_StatusEffectCopy);
        }
        else
        {
            Debug.Log("There is no StatusEffectHandler component ");
        }
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
    private bool GetStatusEffectHandlerComponent(out StatusEffectHandler component)
    {
        if (m_ApplyTo == TargetName.Caster)
        {
            return caster.TryGetComponent<StatusEffectHandler>(out component);
        }
        if (m_ApplyTo == TargetName.Target)
        {
            return target.transform.TryGetComponent<StatusEffectHandler>(out component);
        }
        component = null;
        return false;
    }
}