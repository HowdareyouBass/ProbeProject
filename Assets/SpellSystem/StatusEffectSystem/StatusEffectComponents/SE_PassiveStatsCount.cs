using UnityEngine;

[System.Serializable]
public class SE_PassiveStatsCount : SE_CountComponent
{
    [SerializeField] private PassiveStats m_Stats;

    public override void Init()
    {
        OnEffectApplied += ApplyStats;
        OnEffectDeapplied += (float amountOverpassed) => { DeapplyStats(); };
        base.Init();
    }

    private void ApplyStats()
    {
        targetEntity.ApplyPassive(m_Stats);
    }
    private void DeapplyStats()
    {
        targetEntity.DeapplyPassive(m_Stats);
    }
}