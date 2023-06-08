using UnityEngine;

[System.Serializable]
public class SE_PassiveTimeComponent : SE_TimeComponent
{
    [SerializeField] private PassiveStats m_Stats;

    public override void Init()
    {
        OnEffectApplied += () => targetEntity.ApplyPassive(m_Stats);
        OnEffectDeapplied += () => targetEntity.DeapplyPassive(m_Stats);
        base.Init();
    }
}