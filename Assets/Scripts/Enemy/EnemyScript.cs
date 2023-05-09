using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyScript : EntityScript
{
    [SerializeField] private Race m_Race;
    [SerializeField] private Renderer m_HealthRenderer;

    private Enemy m_Enemy;
    private GameEvent m_OnDeath;

    private void Awake()
    {
        m_Enemy = new Enemy();
        m_Enemy.SetRace(m_Race);
        m_OnDeath = m_Enemy.GetEvent(EntityEventName.OnDeath);
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
        base.Die();
        m_HealthRenderer.enabled = false;
    }

    public override Entity GetEntity()
    {
        Debug.Assert(m_Enemy != null);
        return m_Enemy;
    }
}