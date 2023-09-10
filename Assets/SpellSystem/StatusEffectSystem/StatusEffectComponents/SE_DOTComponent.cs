using UnityEngine;

[System.Serializable]
public class SE_DOTComponent : SE_TimeComponent
{
    [SerializeField] private float m_DamagePerSecond;

    protected override void PerSecond()
    {
        targetEntity.TakeDamage(m_DamagePerSecond);
    }
}