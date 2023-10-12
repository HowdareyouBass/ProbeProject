using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpellInventory))]
public class SpellCaster : MonoBehaviour
{
    private Movement m_Movement;
    private Coroutine m_SpellCasting;
    private EntityEvents m_EntityEvents;
    private EntityController m_Controller;
    private SpellInventory m_Spells;

    public event Action<Spell> OnSpellCast;

    private void Start()
    {
        m_Movement = GetComponent<Movement>();
        m_Spells = GetComponent<SpellInventory>();
        m_EntityEvents = GetComponent<EntityScript>().GetEntity().Events;
        m_Controller = GetComponent<EntityController>();
    }
    private void Update()
    {
        m_Spells.DecreaseSpellsCooldown();
    }

    public void CastSpell(int spellSlot, Target target)
    {
        Spell spell = m_Spells.GetSpell(spellSlot);
        
        if (spell == null)
        {
            Debug.LogWarning("There is no spell in slot" + spellSlot);
            return;
        }
        if (spell.TryGetComponent<S_ActiveSpellComponent>(out var s) && !s.OnCooldown && s.EnoughResources)
        {
            //If spell needs entity as target then we don't cast
            if (spell.HasComponentOfType<S_TargetCastSpellComponent>() && !target.isEntity)
            {
                return;
            }
            m_Controller.StopActions();
            m_SpellCasting = StartCoroutine(CastSpellRoutine(target, spell));
        }
    }
    private IEnumerator CastSpellRoutine(Target target, Spell spell)
    {
        //TODO: Clean this
        // May be do class or interface named Requiremoving
        if (spell.TryGetComponent<S_TargetCastSpellComponent>(out S_TargetCastSpellComponent targetCastSpell))
        {
            yield return m_Movement.FolowUntilInRange(target, targetCastSpell.castRange);
        }
        if (spell.TryGetComponent<S_SpotCastSpell>(out S_SpotCastSpell spotCastSpell))
        {
            yield return m_Movement.FolowUntilInRange(target, spotCastSpell.castRange);
        }
        OnSpellCast.Invoke(spell);
        spell.Cast(target);
        m_EntityEvents.GetEvent(EntityEventName.OnAnySpellCasted).Trigger();
        yield break;
    }

    public void Stop()
    {
        if (m_SpellCasting != null)
            StopCoroutine(m_SpellCasting);
    }
}