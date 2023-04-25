using UnityEngine;

[System.Serializable]
public class Spell : DatabaseItem
{
    public enum Types { none, projectile, directedAtEnemy, directedAtGround, playerCast, passive, custom }

    //These are for all spells
    [SerializeField] private float m_SpellDamage;
    [SerializeField] private Types m_Type = Types.none;
    
    //This is for Projectiles and Directed spells
    [SerializeField] private GameObject m_EffectOnImpact;
    [SerializeField] private float m_CastRange;

    //This is for Projectile only
    [SerializeField] private GameObject m_Effect;
    [SerializeField] private int m_ProjectileSpeed;

    //This is for Directed spells only
    [SerializeField] private bool m_HaveRadiusOnImpact;
    [SerializeField] private float m_RadiusOnImpact;

    //This is for Passive only
    [SerializeField] private PassiveSpellStats m_PassiveStats;
    [SerializeField] private Percents m_Percents;

    //This is for Player cast only
    [SerializeField] private StatusEffect m_StatusEffect;

    public Spell(string name)
    {
        m_Name = name;
    }

    public bool HaveRadiusOnImpact { get => m_HaveRadiusOnImpact; }

    public float GetDamage() { return m_SpellDamage; }
    public Types GetSpellType() { return m_Type; }

    public GameObject GetEffectOnImpact() { return m_EffectOnImpact; }
    public float GetCastRange() { return m_CastRange / 50; }

    public GameObject GetEffect() { return m_Effect; }
    public int GetProjectileSpeed() { return m_ProjectileSpeed; }

    public float GetRadiusOnImpact() { return m_RadiusOnImpact; }

    public PassiveSpellStats GetPassiveStats() { return m_PassiveStats; }
    public Percents GetPercents() { return m_Percents; }

    public StatusEffect GetStatusEffect() { return m_StatusEffect; }
}
