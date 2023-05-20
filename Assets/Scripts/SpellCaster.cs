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
    private ActiveSpell m_Spell;

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
            if (!spellGO.TryGetComponent<ActiveSpell>(out var a))
            {
                Instantiate(spellGO, transform);
            }
        }
    }

    public void CastSpell(int spellSlot, Target target)
    {
        m_Controller.StopActions();
        GameObject spellGO = m_Spells.GetSpell(spellSlot);
        
        if (spellGO.TryGetComponent<ActiveSpell>(out m_Spell))
        {
            m_SpellCasting = StartCoroutine(CastSpellRoutine(target, m_Spell.castRange));
        }
        foreach (ICastable spell in GetComponentsInChildren<ICastable>())
        {
            spell.Cast(transform, target.transform);
        }
    }
    private IEnumerator CastSpellRoutine(Target target, int castRange)
    {
        yield return m_Movement.FolowUntilInRange(target, castRange);
        m_Spell.Cast(transform, target.transform);
        yield break;
    }

    public void Stop()
    {
        if (m_SpellCasting != null)
            StopCoroutine(m_SpellCasting);
    }
}