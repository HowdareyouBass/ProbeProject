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

    private void Start()
    {
        m_Movement = GetComponent<Movement>();
        m_Spells = GetComponent<SpellInventory>();
        m_EntityEvents = GetComponent<EntityScript>().GetEntity().events;
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
        if (spell.TryGetComponent<S_ActiveSpellComponent>(out var s) && !s.OnCooldown)
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
        if (spell.TryGetComponent<S_TargetCastSpellComponent>(out S_TargetCastSpellComponent targetCastSpell))
        {
            yield return m_Movement.FolowUntilInRange(target, targetCastSpell.castRange);
        }
        if (spell.TryGetComponent<S_SpotCastSpell>(out S_SpotCastSpell spotCastSpell))
        {
            yield return m_Movement.FolowUntilInRange(target, spotCastSpell.castRange);
        }
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