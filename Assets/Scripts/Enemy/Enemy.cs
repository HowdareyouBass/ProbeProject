using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEntity
{
    [SerializeField] private Renderer healthRenderer = new Renderer();
    [SerializeField] private Race enemyRace;
    [SerializeField] private GameObject damageEffect;
    private EnemyStats stats;
    private EnemyEquipment equipment;
    private Spell currentSpell;
    [HideInInspector] public bool isDead;

    void Start()
    {
        stats = new EnemyStats();
        stats.ApplyRace(enemyRace);
    }
    public void Damage(float amount)
    {
        GameObject effect = Instantiate(damageEffect, transform.position, transform.rotation);
        effect.transform.SetParent(transform);
        effect.GetComponent<TextMeshPro>().text = amount.ToString();

        stats.TakeDamage(amount);

        healthRenderer.material.SetFloat("_Health", stats.GetCurrentHealth());

        if (stats.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        //healthRenderer.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = false;
        Destroy(transform.gameObject, 0.1f);
    }

    public GameEvent GetOnDeathEvent() { return null; }
    public GameEvent<float> GetOnDamageEvent() { return null; }
    public EntityStats GetStats() { return stats; }
    public EntityEquipment GetEquipment() { return equipment; }
    public float GetAttackCooldown()
    {
        return stats.GetBaseAttackSpeed() * 100 / (stats.GetAttackSpeed());
    }
    public Spell GetSpellFromSlot(int spellSlot) { return equipment.GetSpell(spellSlot); }
    //public Spell GetCurrentSpell() { return currentSpell; }
    //public void SetCurrentSpell(int spellSlot)
    //{
    //    currentSpell = equipment.GetSpell(spellSlot);
    //}
}
