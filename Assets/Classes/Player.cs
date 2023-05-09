public class Player : Entity
{
    private PlayerEquipment m_Equipment;

    public Player()
    {
        m_Equipment = new PlayerEquipment();
    }

    public void SetRace(Race race)
    {
        stats.ApplyRace(race);
    }

    public void EquipItem(Item item)
    {
        m_Equipment.EquipItem(item);
    }
    
    public override float GetAttackCooldown()
    { 
        return stats.baseAttackSpeed * 100 / (m_Equipment.GetAttackSpeed() + stats.attackSpeed);
    }
}