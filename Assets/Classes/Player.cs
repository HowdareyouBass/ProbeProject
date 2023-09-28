public sealed class Player : Entity
{
    public PlayerStats PlayerSpecificStats { get; private set; }
    public override EntityStats Stats { get => PlayerSpecificStats; }

    private PlayerEquipment m_Equipment;

    public Player()
    {
        m_Equipment = new PlayerEquipment();
        PlayerSpecificStats = new PlayerStats();
    }

    public void SetRace(Race race)
    {
        Stats.ApplyRace(race);
    }

    public void EquipItem(Item item)
    {
        m_Equipment.EquipItem(item);
    }

    public override float GetAttackCooldown()
    {
        return Stats.BaseAttackSpeed * 100 / (Stats.AttackSpeed + m_Equipment.GetAttackSpeed());
    }
}