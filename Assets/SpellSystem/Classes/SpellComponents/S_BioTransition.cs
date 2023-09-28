using UnityEngine;

[System.Serializable]
public sealed class S_BioTransition : S_SwitchablePassive
{
    [Range(0, 6)][SerializeField] private float m_SlashingAttackPercent;

    private PassiveStats m_SlashingStats;
    private S_StatusEffectComponent m_RegenDecreaseStatusEffect;

    public override void Init()
    {
        OnSwitch += DisableRegenDecrease;
        targetStats.PureAttack = true;
        m_SlashingStats = new PassiveStats(m_SlashingAttackPercent);
        m_Passive1 = new PassiveStats();
        m_Passive2 = m_SlashingStats;
        base.Init();
    }
    public override void LateInit()
    {
        m_RegenDecreaseStatusEffect = spell.GetComponent<S_StatusEffectComponent>();
        m_RegenDecreaseStatusEffect.Disable();
    }

    private void DisableRegenDecrease()
    {
        if (isSwitched)
        {
            m_RegenDecreaseStatusEffect.Disable();
        }
        else
        {
            m_RegenDecreaseStatusEffect.Enable();
        }
    }
}