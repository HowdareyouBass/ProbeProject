using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveSpellStats
{
    [SerializeField] private int m_AttackSpeed;
    
    public int GetAttackSpeed() { return m_AttackSpeed; }
}
