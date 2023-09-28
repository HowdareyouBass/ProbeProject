[System.Serializable]
public class Enemy : Entity
{
    public EnemyStats EnemySpecificStats { get; private set; }
    public override EntityStats Stats => EnemySpecificStats;

    public Enemy()
    {
        EnemySpecificStats = new EnemyStats();
    }

    public void SetRace(Race race)
    {
        Stats.ApplyRace(race);
    }
}