using UnityEngine;

[System.Serializable]
public class PassiveStats
{
    [field: SerializeField] public float AttackPercent { get; private set; } = 0;
    [field: SerializeField] public int AttackSpeed { get; private set; } = 0;
    [field: SerializeField][field: Range(-10, 10)] public float Regenerate { get; private set; } = 0;
    [field: SerializeField][field: Range(0, 2)] public float RegeneratePercent { get; private set; } = 0;
    [field: SerializeField] public float CooldownReductionInPercents { get; private set; } = 0;
    [field: SerializeField] public float HitChanceReduction { get; private set; } = 0;

    public PassiveStats(float attackPercent)
    {
        AttackPercent = attackPercent;
    }
    public PassiveStats(){}
}