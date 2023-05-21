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
            if (m_SpellInventory.GetSpell(i))
                m_Text.text += $"Spell {i + 1}: {m_SpellInventory.GetSpell(i).GetComponent<ActiveSpell>().currentCooldown}";
            m_Text.text += "\n"; 
        }
    }
}
