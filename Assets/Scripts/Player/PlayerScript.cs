using UnityEngine;

public sealed class PlayerScript : EntityScript
{
    [SerializeField] private Race m_Race;
    [SerializeField] private PlayerLevelRequirementsSO m_LevelDb;

    private Player m_Player;

    public PlayerEvents events { get; private set; }

    private GameEvent m_OnDeath;

    private void Awake()
    {
        events = new PlayerEvents();
        m_Player = new Player();
        m_Player.SetRace(m_Race);
        m_OnDeath = GetEntity().Events.GetEvent(EntityEventName.OnDeath);
    }
    private void OnEnable()
    {
        m_OnDeath?.Subscribe(Die);
    }
    private void OnDisable()
    {
        m_OnDeath?.Unsubscribe(Die);
    }
    protected override void Die()
    {
        Destroy(GetComponent<PlayerAnimationsController>());
        base.Die();
    }
    
    public override Entity GetEntity()
    {
        return m_Player;
    }
}