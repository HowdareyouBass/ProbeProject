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
        UpdateSpells();
    }
    private void UpdateSpells()
    {
        foreach (SpellScript spell in gameObject.GetComponentsInChildren<SpellScript>())
        {
            Destroy(spell.gameObject);
        }
        for (int i = 0; i < SpellInventory.MAXIMUM_SPELLS; i++)
        {
            GameObject spellGO = m_Spells.GetSpell(i);
            if (spellGO == null) continue;
            if (!spellGO.TryGetComponent<TargetCastSpell>(out var a))
            {
                Instantiate(spellGO, transform);
            }
        }
    }

    public void CastSpell(int spellSlot, Target target)
    {
        m_Controller.StopActions();
        GameObject spellGO = m_Spells.GetSpell(spellSlot);
        
        if (spellGO.TryGetComponent<TargetCastSpell>(out TargetCastSpell activeSpell))
        {
            m_SpellCasting = StartCoroutine(CastSpellRoutine(target, activeSpell));
            return;
        }
        int i = 0;
        foreach (ICastable spell in GetComponentsInChildren<ICastable>())
        {
            if (i == spellSlot)
            {
                spell.Cast(transform, target.transform);
            }
            i++;
        }
    }
    private IEnumerator CastSpellRoutine(Target target, TargetCastSpell spell)
    {
        yield return m_Movement.FolowUntilInRange(target, spell.castRange);
        spell.Cast(transform, target.transform);
        yield break;
    }

    public void Stop()
    {
        if (m_SpellCasting != null)
            StopCoroutine(m_SpellCasting);
    }
}