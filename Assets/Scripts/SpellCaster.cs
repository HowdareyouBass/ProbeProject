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
        m_Controller.StopActions();
        m_SpellCasting = StartCoroutine(CastSpellRoutine(target, m_Spells.GetSpell(spellSlot)));
    }
    private IEnumerator CastSpellRoutine(Target target, Spell spell)
    {
        if (spell.TryGetComponent<TargetCastSpellComponent>(out TargetCastSpellComponent targetCastSpell))
        {
            yield return m_Movement.FolowUntilInRange(target, targetCastSpell.castRange);
        }
        spell.GetComponent<ActiveSpellComponent>().TryCast();
        yield break;
    }

    public void Stop()
    {
        if (m_SpellCasting != null)
            StopCoroutine(m_SpellCasting);
    }
}