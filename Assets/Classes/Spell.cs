using UnityEngine;

[System.Serializable]
public class Spell
{
    public enum Types { none, projectile, directedAtEnemy, directedAtGround, playerCast, passive, passiveSwitchable }

    //For all spells
    [SerializeField] private string m_Name;
    [SerializeField] private float m_SpellDamage;
    [SerializeField] private Types m_Type = Types.none;
    
    //For Projectiles and Directed spells
    [SerializeField] private GameObject m_EffectOnImpact;
    [SerializeField] private float m_CastRange;

    //For Projectile only
    [SerializeField] private GameObject m_Effect;
    [SerializeField] private int m_ProjectileSpeed;

    //For Directed spells only
    [SerializeField] private bool m_HaveRadiusOnImpact;
    [SerializeField] private float m_RadiusOnImpact;

    //For Passive only
    [SerializeField] private PassiveSpellStats m_PassiveStats;
    [SerializeField] private Percents m_Percents;

    //For Player cast only
    [SerializeField] private StatusEffect m_StatusEffect;

    //For Switchable passive only
    [SerializeField] private SwitchablePassiveSpell m_SwitchableStats; 


    public Spell(string name)
    {
        m_Name = name;
    }

    public void SwitchPassive()
    {
        m_SwitchableStats.Switch();
    }

    public bool HaveRadiusOnImpact { get => m_HaveRadiusOnImpact; }

    public string GetName() { return m_Name; }
    public float GetDamage() { return m_SpellDamage; }
    public Types GetSpellType() { return m_Type; }

    public GameObject GetEffectOnImpact() { return m_EffectOnImpact; }
    public float GetCastRange() { return m_CastRange / 50; }

    public GameObject GetEffect() { return m_Effect; }
    public int GetProjectileSpeed() { return m_ProjectileSpeed; }

    public float GetRadiusOnImpact() { return m_RadiusOnImpact; }

    public PassiveSpellStats GetPassiveStats() 
    {
        if (m_Type == Types.passiveSwitchable)
        {
            return m_SwitchableStats.GetStats();
        }
        return m_PassiveStats;
    }
    public Percents GetPercents() 
    {
        if (m_Type == Types.passiveSwitchable)
        {
            return m_SwitchableStats.GetPercents();
        }
        return m_Percents; 
    }

    public StatusEffect GetStatusEffect() { return m_StatusEffect; }
}
