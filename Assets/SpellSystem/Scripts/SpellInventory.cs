using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    [SerializeField] private Spell[] m_Spells;
    public const int MAXIMUM_SPELLS = 5;

    //TODO: Init spell on pickup
    private void Start()
    {
        foreach(Spell spell in m_Spells)
        {
            if (spell != null)
            {
                spell.Init(transform);
            }
        }
    }

    public Spell GetSpell(int spellSlot)
    {
        if (spellSlot < 0 || spellSlot > MAXIMUM_SPELLS)
            throw new System.ArgumentOutOfRangeException();
        return m_Spells[spellSlot];
    }
    public void DecreaseSpellsCooldown()
    {
        foreach (Spell spell in m_Spells)
        {
            if (spell != null)
                spell.GetComponent<ActiveSpellComponent>().DecreaseCooldown();
        }
    }
}