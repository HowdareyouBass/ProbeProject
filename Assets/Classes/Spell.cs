using UnityEngine;

[System.Serializable]
public class Spell : DatabaseItem
{
    public enum Types { none, projectile, directedAtEnemy, directedAtGround, playerCast, passive, custom }

    //These are for all spells
    [SerializeField] private float m_SpellDamage;
    [SerializeField] private Types m_Type = Types.none;
    
    //This is for Projectiles and Directed spells
    [SerializeField] private GameObject m_Effect;

    //This is for Projectile only
    [SerializeField] private GameObject m_EffectOnImpact;
    [SerializeField] private int m_SpeedOfProjectile;

    //This is for Passive only
    [SerializeField] private PlayerStats m_PassiveStats;
    [SerializeField] private Percents m_Percents;

    public Spell(string name)
    {
        m_Name = name;
    }

    public float GetDamage()
    {
        return m_SpellDamage;
    }

    public GameObject GetEffect()
    {
        return m_Effect;
    }
    public GameObject GetEffectOnImpact()
    {
        return m_EffectOnImpact;
    }

    public int GetSpeed()
    {
        return m_SpeedOfProjectile;
    }
    public Types GetSpellType()
    {
        return m_Type;
    }

    public PlayerStats GetPassiveStats()
    {
        return m_PassiveStats;
    }
    public Percents GetPercents()
    {
        return m_Percents;
    }
}
