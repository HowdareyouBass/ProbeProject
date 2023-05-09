using UnityEngine;

public sealed class PlayerScript : EntityScript
{
    [SerializeField] private Race m_Race;
    private Player m_Player;
    private void Awake()
    {
        m_Player = new Player();
        m_Player.SetRace(m_Race);
    }

    public override Entity GetEntity()
    {
        return m_Player;
    }
}
