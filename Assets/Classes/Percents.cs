using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Percents
{
    [Range(0, 10)] [SerializeField] private float m_AttackPercent;
    [Range(0, 10)] [SerializeField] private float m_MaxHealthPercent;

    public float GetAttackPercent(){ return m_AttackPercent; }
    public float GetMaxHealthPercent(){ return m_MaxHealthPercent; }

}
