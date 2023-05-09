public class EntityStats
{
    //So i've found this post on stack overflow (https://stackoverflow.com/questions/3182653/are-protected-members-fields-really-that-bad) 
    //I have PlayerStats and EnemyStats i don't yet know do they deffer or they don't
    //Not to repeat the code i did this monstrocity
    //Please if you see this and you're good at programming
    //Tell me how to design that so i don't need to do this
    public float currentHealth { get; private set; } = 0;
    public float maxHealth { get; private set; } = 0;
    public float attackSpeed { get; private set; } = 20;
    public float baseAttackSpeed { get; private set; } = 1;
    public float attackDamage { get; private set; } = 0;
    public int attackRange { get; private set; } = 10;
    public float evasion { get; private set; } = 0;
    public float regen { get => m_RegeneratePercent * m_RegenerateAmount; }

    private float m_RegenerateAmount = 1;
    private float m_RegeneratePercent = 1;


    public void Heal(float amount)
    {
        if (currentHealth + amount < maxHealth)
        {
            currentHealth += amount;
        }
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    public void ApplyRace(Race race)
    {
        currentHealth = race.health;
        maxHealth = race.health;
        attackDamage = race.attack;
        attackRange = race.attackRange;
        attackSpeed = race.attackSpeed;
        baseAttackSpeed = race.baseAttackSpeed;
        evasion = race.evasion;
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        StatusEffectStats stats = effect.stats;

        attackSpeed += stats.attackSpeed;
        m_RegenerateAmount += stats.regenerate;
        m_RegeneratePercent += stats.regeneratePercent;
    }
    public void DeapplyStatusEffect(StatusEffect effect)
    {
        StatusEffectStats stats = effect.stats;

        attackSpeed -= stats.attackSpeed;
        m_RegenerateAmount -= stats.regenerate;
        m_RegeneratePercent -= stats.regeneratePercent;
    }
}
