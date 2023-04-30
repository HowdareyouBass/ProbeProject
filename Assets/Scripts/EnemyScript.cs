using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityScript
{
    [SerializeField] private Race race;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private Renderer healthRenderer;

    private Enemy enemy;
    public bool isDead { get; private set; }

    private void Awake()
    {
        enemy = new Enemy();
        enemy.SetRace(race);
    }
    private void Start()
    {
        enemy.events.OnEnemyDeath.Subscribe(Die);
        enemy.events.OnEnemyDamaged.Subscribe(Damage);
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
        Destroy(transform.gameObject, 0.1f);
    }
    public override Entity GetEntity()
    {
        Debug.Assert(enemy != null);
        return enemy;
    }
}
