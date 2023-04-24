using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    [SerializeField] private float m_Duration;
    [SerializeField] private StatusEffectStats m_Stats;

    public IEnumerator StartEffect(PlayerBehaviour player)
    {
        player.ApplyStatusEffect(this);
        yield return new WaitForSeconds(m_Duration);
        player.DeapplyStatusEffect(this);
    }

    public StatusEffectStats GetStatusEffectStats() { return m_Stats; }
}
