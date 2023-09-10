using TMPro;
using UnityEngine;

public class DebugStatusEffectUpdate : MonoBehaviour
{
    [SerializeField] private StatusEffectHandler m_StatusEffectHandler;
    private TextMeshProUGUI m_Text;

    private void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        m_Text.text = "";
        int i = 1;
        foreach (StatusEffect effect in m_StatusEffectHandler.Effects)
        {
            m_Text.text = $"{i} {effect.Name} ";
            if (effect.TryGetComponent<SE_TimeComponent>(out SE_TimeComponent time))
            {
                m_Text.text += $"TimeLeft: {time.LeftTime}\n";
            }
            else if (effect.TryGetComponent<SE_CountComponent>(out SE_CountComponent count))
            {
                m_Text.text += $"Count: {count.CurrentCount}\n";
            }
        }
    }
}
