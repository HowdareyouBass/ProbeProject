using UnityEngine;

public sealed class PlayerScript : EntityScript
{
    [SerializeField] private Race m_Race;
    [SerializeField] private PlayerLevelRequirementsSO m_LevelDb;

    private Player m_Player;

    public PlayerEvents events { get; private set; }

    private void Awake()
    {
        events = new PlayerEvents();
        m_Player = new Player();
        m_Player.SetRace(m_Race);
    }

    public override Entity GetEntity()
    {
        return m_Player;
    }
}