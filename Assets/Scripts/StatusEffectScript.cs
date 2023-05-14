using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpellScript))]
public class StatusEffectScript : SpellComponent
{
    private enum StatusEffectTarget { Caster, Enemy, EventReturn }

    [SerializeField] private SpellEventName m_SpellEventName;
    [SerializeField] private EntityEventName m_EntityEventName;
    [SerializeField] private StatusEffectTarget m_ApplyTo;
    [SerializeField] private StatusEffect m_StatusEffect;

    private Transform m_TargetTransform;
    private Entity m_Entity;
    private GameEvent<Transform> m_EntityEvent;
    private GameEvent m_SpellEvent;

    private void Awake() 
    {
        m_TargetTransform = GetTargetTransform(m_ApplyTo);
        m_Entity = m_TargetTransform.GetComponent<EntityScript>().GetEntity();
        m_SpellEvent = spellScript.events.GetEvent(m_SpellEventName);
        m_EntityEvent = m_Entity.events.GetEvent<Transform>(m_EntityEventName, true);
    }

    private void OnEnable()
    {
        m_SpellEvent.Subscribe(ApplyStatusEffect);
        if (m_EntityEventName != EntityEventName.OnAttack)
        {
            if (m_EntityEventName != EntityEventName.None)
                Debug.LogError("Event named " + m_EntityEventName.ToString() + " don't work yet.");
            return;
        }
        m_EntityEvent.Subscribe(ApplyStatusEffectToEventReturn);
    }
    private void OnDisable()
    {
        m_SpellEvent.Unsubscribe(ApplyStatusEffect);
        if (m_EntityEventName != EntityEventName.OnAttack)
        {
            if (m_EntityEventName != EntityEventName.None)
                Debug.LogError("Event named " + m_EntityEvent.ToString() + " don't work yet.");
            return;
        }
        m_EntityEvent.Unsubscribe(ApplyStatusEffectToEventReturn);
    }

    private void ApplyStatusEffect()
    {
        m_TargetTransform.GetComponent<StatusEffectHandler>().AddEffect(m_StatusEffect);
        //StartCoroutine(m_StatusEffect.StartEffectRoutine(m_Entity));
    }
    private void ApplyStatusEffectToEventReturn(Transform target)
    {
        if (m_ApplyTo == StatusEffectTarget.EventReturn)
        {
            //StartCoroutine(m_StatusEffect.StartEffectRoutine(target.GetComponent<EntityScript>().GetEntity()));
            target.GetComponent<StatusEffectHandler>().AddEffect(m_StatusEffect);
            return;
        }
        ApplyStatusEffect();
    }

    private Transform GetTargetTransform(StatusEffectTarget statusEffectTarget)
    {
        switch (statusEffectTarget)
        {
            case StatusEffectTarget.Caster:
            {
                return caster;
            }
            case StatusEffectTarget.Enemy:
            {
                return target;
            }
            case StatusEffectTarget.EventReturn:
            {
                return null;
            }
            default:
            {
                throw new System.Exception("There is no transform of this target: " + statusEffectTarget.ToString());
            }
        }
    }
}
