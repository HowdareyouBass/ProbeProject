using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    [SerializeField] private GameObject m_Spell1, m_Spell2, m_Spell3, m_Spell4, m_Spell5;
    private GameObject[] m_Spells;
    public const int MAXIMUM_SPELLS = 5;

    private void Awake()
    {
        m_Spells = new GameObject[]
        {
            m_Spell1,
            m_Spell2,
            m_Spell3,
            m_Spell4,
            m_Spell5
        };
    }

    public GameObject GetSpell(int spellSlot)
    {
        if (spellSlot < 0 || spellSlot > MAXIMUM_SPELLS)
            throw new System.ArgumentOutOfRangeException();
        return m_Spells[spellSlot];
    }
}