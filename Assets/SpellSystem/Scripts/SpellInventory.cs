using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    [SerializeField] private Spell[] m_Spells;
    //Needed for using one ScriptableObject for every inventory
    private Spell[] m_SpellCopies;
    public const int MAXIMUM_SPELLS = 5;

    //TODO: Init spell on pickup
    private void Start()
    {
        m_SpellCopies = new Spell[m_Spells.Length];
        for (int i = 0; i < m_SpellCopies.Length; i++)
        {
            if (m_Spells[i] != null)
                m_SpellCopies[i] = Instantiate(m_Spells[i]);
            else
                m_SpellCopies[i] = null;
        }
        foreach (Spell spell in m_SpellCopies)
        {
            if (spell != null)
                spell.Init(transform);
        }
    }

    public Spell GetSpell(int spellSlot)
    {
        if (spellSlot < 0 || spellSlot > m_Spells.Length - 1)
            throw new System.ArgumentOutOfRangeException();
        return m_SpellCopies[spellSlot];
    }
    public void DecreaseSpellsCooldown()
    {
        foreach (Spell spell in m_SpellCopies)
        {
            if (spell != null)
                spell.GetComponent<S_ActiveSpellComponent>().DecreaseCooldown();
        }
    }
}