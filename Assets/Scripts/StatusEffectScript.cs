using UnityEngine;

[RequireComponent(typeof(SpellScript))]
public class StatusEffectScript : SpellComponent
{
    private enum StatusEffectTarget { Caster, Enemy }

    [SerializeField] private SpellEventName m_SpellEvent;
    [SerializeField] private EntityEventName m_EntityEvent;
    [SerializeField] private StatusEffectTarget m_ApplyTo;

    [SerializeField] private StatusEffect m_StatusEffect;

    private void OnEnable()
    {
        spellScript.GetEvent(m_SpellEvent).Subscribe(ApplyStatusEffect);
        if (m_EntityEvent != EntityEventName.OnAttack)
        {
            Debug.LogError("Event named " + m_EntityEvent.ToString() + " don't work yet.");
            return;
        }
        caster.GetComponent<EntityScript>().GetEntity().GetEvent<Transform>(m_EntityEvent, true).Subscribe(ApplyStatusEffectOnTarget);
    }
    private void OnDisable()
    {
        spellScript.GetEvent(m_SpellEvent).Unsubscribe(ApplyStatusEffect);
        if (m_EntityEvent != EntityEventName.OnAttack)
        {
            Debug.LogError("Event named " + m_EntityEvent.ToString() + " don't work yet.");
            return;
        }
        caster.GetComponent<EntityScript>().GetEntity().GetEvent<Transform>(m_EntityEvent, true).Unsubscribe(ApplyStatusEffectOnTarget);
    }

    private void ApplyStatusEffect()
    {
        if (m_ApplyTo == StatusEffectTarget.Caster)
        {
            StartCoroutine(m_StatusEffect.StartEffectRoutine(caster.GetComponent<EntityScript>().GetEntity()));
        }
        if (m_ApplyTo == StatusEffectTarget.Enemy)
        {
            StartCoroutine(m_StatusEffect.StartEffectRoutine(target.GetComponent<EntityScript>().GetEntity()));
        }
    }
    private void ApplyStatusEffectOnTarget(Transform target)
    {
        if (m_ApplyTo == StatusEffectTarget.Caster)
        {
            StartCoroutine(m_StatusEffect.StartEffectRoutine(caster.GetComponent<EntityScript>().GetEntity()));
        }
        if (m_ApplyTo == StatusEffectTarget.Enemy)
        {
            StartCoroutine(m_StatusEffect.StartEffectRoutine(target.GetComponent<EntityScript>().GetEntity()));
        }
    }
}
