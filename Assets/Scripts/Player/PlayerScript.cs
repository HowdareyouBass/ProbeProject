using UnityEngine;
public class PlayerScript : EntityScript
{
    [SerializeField] private Race race;
    private Player player;
    private void Awake()
    {
        player = new Player();
        player.SetRace(race);
    }

    public override Entity GetEntity()
    {
        return player;
    }
    public Player GetPlayer()
    {
        return player;
    }
}
