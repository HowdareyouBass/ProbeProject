using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
    [SerializeField] private SpellInventory m_SpellInventory;
    private TextMeshProUGUI m_Text;

    private void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        m_Text.text = "";
        for (int i = 0; i < SpellInventory.MAXIMUM_SPELLS; i++)
        {
            if (m_SpellInventory.GetSpell(i) != null)
                m_Text.text += $"Spell {i + 1}: {Mathf.Round(m_SpellInventory.GetSpell(i).GetComponent<ActiveSpellComponent>().currentCooldown)}";
            m_Text.text += "\n"; 
        }
    }
}
