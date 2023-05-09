[System.Serializable]
public class Enemy : Entity
{
    public void SetRace(Race race)
    {
        stats.ApplyRace(race);
    }
}