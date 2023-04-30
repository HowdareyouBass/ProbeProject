using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityScript
{
    [SerializeField] private Race race;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private Renderer healthRenderer;

    private Enemy enemy;
    private GameEvent<float> OnDamaged;
    private GameEvent OnDeath;
    public bool isDead { get; private set; }

    private void Awake()
    {
        enemy = new Enemy();
        enemy.SetRace(race);
        OnDamaged = enemy.GetEvents()[EventName.OnDamaged] as GameEvent<float>;
        OnDeath = enemy.GetEvents()[EventName.OnDeath];
    }
    private void OnEnable()
    {
        OnDeath?.Subscribe(Die);
        OnDamaged?.Subscribe(Damage);
    }
    private void OnDisable()
    {
        OnDeath?.Unsubscribe(Die);
        OnDamaged?.Unsubscribe(Damage);
    }

    private void Damage(float amount)
    {
        GameObject effect = Instantiate(damageEffect, transform.position, transform.rotation);
        effect.transform.SetParent(transform);
        effect.GetComponent<TextMeshPro>().text = amount.ToString();
    }
    private void Die()
    {
        isDead = true;
        healthRenderer.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        Destroy(transform.gameObject, 1f);
    }
    public override Entity GetEntity()
    {
        Debug.Assert(enemy != null);
        return enemy;
    }
}
