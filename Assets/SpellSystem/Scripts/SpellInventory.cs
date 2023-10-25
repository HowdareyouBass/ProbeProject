using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    [SerializeField] private Spell[] m_Spells; // For Debug purposes
    [SerializeField] private Spell[] m_SpellCopies; // Actual Spell Invenotry
    public const int MAXIMUM_SPELLS = 5;

    //TODO: Init spell on pickup
    private void Start()
    {
        m_SpellCopies = new Spell[m_Spells.Length];
        for (int i = 0; i < m_SpellCopies.Length; i++) 
        {
            if (m_Spells[i] != null)
            {
                m_SpellCopies[i] = Instantiate(m_Spells[i]);
                m_SpellCopies[i].Init(transform, i);
            }
            else
            {
                m_SpellCopies[i] = null;
            }
        }
    }
    public void SwitchSpell(int spellSlot, Spell spell, bool goOnCooldown)
    {
        if (spellSlot < 0 || spellSlot >= m_Spells.Length)
            throw new System.ArgumentOutOfRangeException();
        if (m_SpellCopies[spellSlot] == null)
            throw new System.Exception("No spell in this slot " + spellSlot.ToString());
        Spell spellCopy = Instantiate(spell);
        m_SpellCopies[spellSlot] = spellCopy;
        m_SpellCopies[spellSlot].Init(transform, spellSlot);
        if (m_SpellCopies[spellSlot].TryGetComponent<S_ActiveSpellComponent>(out S_ActiveSpellComponent activeSpellComponent) && goOnCooldown)
        {
            activeSpellComponent.GoOnCooldown();
        }
    }

    public bool AllSpellsOnCooldown()
    {
        foreach (Spell spell in m_SpellCopies)
        {
            if (spell == null) continue;
            if (spell.TryGetComponent<S_ActiveSpellComponent>(out var activeSpell) && !activeSpell.OnCooldown)
            {
                return false;
            }
        }
        return true;
    }
    public int GetIndexOfFirstSpellNotOnCooldown()
    {
        for (int i = 0; i < m_SpellCopies.Length; i++)
        {
            if (m_SpellCopies[i] == null) continue;
            if (m_SpellCopies[i].TryGetComponent<S_ActiveSpellComponent>(out var activeSpell) && !activeSpell.OnCooldown)
            {
                return i;
            }
        }
        Debug.LogWarning("All spells are on cooldown");
        return 0;
    }

    public Spell GetSpell(int spellSlot)
    {
        if (spellSlot < 0 || spellSlot >= m_Spells.Length)
            throw new System.ArgumentOutOfRangeException();
        return m_SpellCopies[spellSlot];
    }
    public void DecreaseSpellsCooldown()
    {
        foreach (Spell spell in m_SpellCopies)
        {
            if (spell != null && spell.TryGetComponent<S_ActiveSpellComponent>(out S_ActiveSpellComponent activeSpellComponent))
            {
                activeSpellComponent.DecreaseCooldown();
            }   
        }
    }
}