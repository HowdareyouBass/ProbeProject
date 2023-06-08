using UnityEngine;

[System.Serializable]
public class PassiveStats
{
    [field: SerializeField] public float attackPercent { get; private set; }
    [field: SerializeField] public int attackSpeed { get; private set; }
    [field: SerializeField][field: Range(-10, 10)] public float regenerate { get; private set; } = 0;
    [field: SerializeField][field: Range(0, 2)] public float regeneratePercent { get; private set; } = 0;

    public PassiveStats(
        float _attackPercent = 0,
        int _attackSpeed = 0,
        float _regenerate = 0,
        float _regeneratePercent = 0)
    {
        attackPercent = _attackPercent;
        attackSpeed = _attackSpeed;
        regenerate = _regenerate;
        regeneratePercent = _regeneratePercent;
    }
}