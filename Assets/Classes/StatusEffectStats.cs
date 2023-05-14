using UnityEngine;

[System.Serializable]
public class StatusEffectStats
{
    [SerializeField] private int m_AttackSpeed;
    [Range(-10, 10)][SerializeField] private float m_Regenerate = 0;
    [Range(0, 2)][SerializeField] private float m_RegeneratePercent = 0;

    public float attackSpeed { get => m_AttackSpeed; }
    public float regenerate { get => m_Regenerate; }
    public float regeneratePercent { get => m_RegeneratePercent; }
}