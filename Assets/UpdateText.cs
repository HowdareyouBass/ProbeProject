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
            Spell spell = m_SpellInventory.GetSpell(i);
            if (spell != null && spell.TryGetComponent<S_ActiveSpellComponent>(out S_ActiveSpellComponent activeSpellComponent))
            {
                m_Text.text += spell.Name;
                m_Text.text += $" slot:{i + 1} : {Mathf.Round(activeSpellComponent.CurrentCooldown)}";
            }
            else
            {
                m_Text.text += "No spell in slot: " + (i+1).ToString();
            }
            m_Text.text += "\n";
        }
    }
}
