using UnityEngine;

public class EntityStats
{
    //TODO: may be instead of float use some kind of class just not to use -= or += and use reduce and add functions
    public float CurrentHealth { get; private set; } = 0;
    public float MaxHealth { get; private set; } = 0;
    public float CurrentStamina { get; private set; } = 0;
    public float MaxStamina { get; private set; } = 0;

    public float AttackSpeed { get; private set; } = 20;
    public float BaseAttackSpeed { get; private set; } = 1;
    public int AttackRange { get; private set; } = 10;
    public float Evasion { get; private set; } = 0;
    public float HitChance { get; private set; } = 1;
    public float SpellCooldownCoefficient { get; private set; } = 1;
    public float AttackDamage { get => m_Attack * (PureAttack ? 1 : 0.1f) * m_AttackPercent; } //TODO: стили маг и физ ( рендж и ближ )
    public float Regeneration { get => m_RegeneratePercent * m_RegenerateAmount; }

    private float m_Attack = 0;
    private float m_RegenerateAmount = 1;
    private float m_RegeneratePercent = 1;
    private float m_AttackPercent = 1;
    
    public bool PureAttack = false;
    public bool BarrierIsSet = false;

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }
    public void DecreaseHealth(float amount)
    {   
        CurrentHealth -= amount;
        Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }
    public void SpendStamina(float amount)
    {
        CurrentStamina -= amount;
        Mathf.Clamp(CurrentStamina, 0, MaxStamina);
    }

    public void ApplyRace(Race race)
    {
        CurrentHealth = race.Health;
        CurrentStamina = race.Stamina;
        MaxHealth = race.Health;
        MaxStamina = race.Stamina;
        
        m_Attack = race.Attack;
        AttackRange = race.AttackRange;
        AttackSpeed = race.AttackSpeed;
        BaseAttackSpeed = race.BaseAttackSpeed;
        Evasion = race.Evasion;
    }
    //TODO: Rename this func
    public void AddPassiveStats(PassiveStats stats)
    {
        m_AttackPercent += stats.AttackPercent;
        AttackSpeed += stats.AttackSpeed;
        m_RegenerateAmount += stats.Regenerate;
        m_RegeneratePercent += stats.RegeneratePercent;
        SpellCooldownCoefficient -= stats.CooldownReductionInPercents;
        HitChance -= stats.HitChanceReduction;
    }
    
    public void SubtractPassiveStats(PassiveStats stats)
    {
        m_AttackPercent -= stats.AttackPercent;
        AttackSpeed -= stats.AttackSpeed;
        m_RegenerateAmount -= stats.Regenerate;
        m_RegeneratePercent -= stats.RegeneratePercent;
        SpellCooldownCoefficient += stats.CooldownReductionInPercents;
        HitChance += stats.HitChanceReduction;
    }

    public void AddAttackPercent(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentException(nameof(amount));
        m_AttackPercent += amount;
    }
    public void SubtractAttackPercent(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentException(nameof(amount));
        m_AttackPercent -= amount;
    }
}
