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
            spellGO.GetComponent<SpellScript>().slot = i;
            Instantiate(spellGO, transform);
        }
    }

    public void CastSpell(int spellSlot, Target target)
    {
        m_Controller.StopActions();
        GameObject spellGO = m_Spells.GetSpell(spellSlot);

        foreach (SpellScript spell in GetComponentsInChildren<SpellScript>())
        {
            if (spell.slot == spellSlot)
            {
                m_SpellCasting = StartCoroutine(CastSpellRoutine(target, spell));
            }
        }
    }
    private IEnumerator CastSpellRoutine(Target target, SpellScript spell)
    {   
        Debug.Log("dH");
        if (spell.TryGetComponent<TargetCastSpell>(out TargetCastSpell _spell))
        {
            Debug.Log("H");
            yield return m_Movement.FolowUntilInRange(target, _spell.castRange);
        }
        spell.TryCast(transform, target.transform);
        yield break;
    }

    public void Stop()
    {
        if (m_SpellCasting != null)
            StopCoroutine(m_SpellCasting);
    }
}