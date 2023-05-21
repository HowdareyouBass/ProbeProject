using UnityEngine;

public class SwitchDamageType : SpellComponent
{
    [SerializeField] private float m_SlashingDamagePercent = 0.1f;

    private bool m_IsFirstEqiuped;
    private StatusEffectScript m_StatusEffect;
    private EntityStats m_EntityStats;

    private void Awake()
    {
        m_EntityStats = casterEntity.stats;
        m_EntityStats.pureAttack = true;
        m_StatusEffect = GetComponent<StatusEffectScript>();
    }
    private void OnEnable()
    {
        spellScript.events.GetEvent(SpellEventName.OnCast).Subscribe(Switch);
    }
    private void OnDisable()
    {
        spellScript.events.GetEvent(SpellEventName.OnCast).Unsubscribe(Switch);
    }
    private void OnDestroy()
    {
        m_EntityStats.pureAttack = false;
    }

    private void Switch()
    {
        m_IsFirstEqiuped = !m_IsFirstEqiuped;
        if (m_IsFirstEqiuped)
        {
            m_EntityStats.AddAttackPercent(m_SlashingDamagePercent);
            m_StatusEffect.enabled = false;
        }
        else
        {
            m_EntityStats.SubtractAttackPercent(m_SlashingDamagePercent);
            m_StatusEffect.enabled = true;
        }
    }
}