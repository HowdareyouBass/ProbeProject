public class SwitchDamageType : SpellComponent
{
    private bool m_IsFirstEqiuped;
    private StatusEffectScript m_StatusEffect;

    private void Awake()
    {
        m_StatusEffect = GetComponent<StatusEffectScript>();
    }
    private void OnEnable()
    {
        spellScript.GetEvent(SpellEventName.OnCast).Subscribe(Switch);
    }
    private void OnDisable()
    {
        spellScript.GetEvent(SpellEventName.OnCast).Unsubscribe(Switch);
    }

    private void Switch()
    {
        m_IsFirstEqiuped = !m_IsFirstEqiuped;
        if (m_IsFirstEqiuped)
        {
            m_StatusEffect.enabled = false;
        }
        else
        {
            m_StatusEffect.enabled = true;
        }
    }
}
