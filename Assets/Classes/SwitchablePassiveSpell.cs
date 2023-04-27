using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SwitchablePassiveSpell
{
    private bool chosenId = true;
    [SerializeField] private PassiveSpellStats[] m_Stats = new PassiveSpellStats[2];
    [SerializeField] private Percents[] m_Percents = new Percents[2];

    public void Switch()
    {
        chosenId = !chosenId;
    }

    public PassiveSpellStats GetStats() 
    {
        if (chosenId)
        {
            return m_Stats[0];
        }
        else
        {
            return m_Stats[1];
        }
    }

    public Percents GetPercents() 
    {
        if (chosenId)
        {
            return m_Percents[0];
        }
        else
        {
            return m_Percents[1];
        }
    }
}
