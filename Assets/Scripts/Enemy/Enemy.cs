using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEntity
{
    [SerializeField] private Renderer healthRenderer = new Renderer();
    [SerializeField] private GameEventListener OnEnemyDeath;
    [SerializeField] private EnemyType type;
    [SerializeField] private GameObject damageEffect;
    private EnemyStats stats;
    private EnemyEquipment equipment;
    private Spell currentSpell;
    [HideInInspector] public bool isDead;

    void Start()
    {
        OnEnemyDeath.onEventTriggered += (()=> isDead = true);

        stats = new EnemyStats();
        stats.SetType(type);
    }
    public void Damage(float amount)
    {
        GameObject effect = Instantiate(damageEffect, transform.position, transform.rotation);
        effect.transform.SetParent(transform);
        effect.GetComponent<TextMeshPro>().text = amount.ToString();

        stats.Damage(amount);

        healthRenderer.material.SetFloat("_Health", stats.GetCurrentHealth());

        if (stats.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }
    public void CastSpell(RaycastHit target)
    {
        if (currentSpell == null)
        {
            Debug.LogWarning("No spell in a slot ");
            return;
        }

        #if UNITY_EDITOR
        if (currentSpell.GetSpellType() == Spell.Types.none)
        {
            Debug.LogWarning("Spell Type is none");
            return;
        }
        #endif

        if (currentSpell.GetSpellType() == Spell.Types.projectile)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffect(), transform.position, transform.rotation);
            // If spell type is projectile then we move projectile
            Rigidbody rb = castEffect.AddComponent<Rigidbody>();
            rb.useGravity = false;

            //Collider so we can interract with enemy
            SphereCollider collider = castEffect.AddComponent<SphereCollider>();
            collider.radius = 0.4f;
            collider.isTrigger = true;

            //And projectile component
            Projectile spellProjectileComponent = castEffect.AddComponent<Projectile>();
            spellProjectileComponent.spell = currentSpell;
            spellProjectileComponent.target = target;
        }
        if (currentSpell.GetSpellType() == Spell.Types.directedAtEnemy)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffectOnImpact(), target.transform.position, Quaternion.identity);
            if (currentSpell.HaveRadiusOnImpact)
            {
                Explosion spellExplosionComponent = castEffect.AddComponent<Explosion>();
                spellExplosionComponent.spell = currentSpell;
            }
            GetComponent<IEntity>().DamageTarget(target);
        }
        if (currentSpell.GetSpellType() == Spell.Types.directedAtGround)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffectOnImpact(), target.point, Quaternion.identity);
            Explosion spellExplosionComponent = castEffect.AddComponent<Explosion>();
            spellExplosionComponent.spell = currentSpell;
        }
        if (currentSpell.GetSpellType() == Spell.Types.playerCast)
        {
            Debug.Log("Casted something on yourself");
            StartCoroutine(currentSpell.GetStatusEffect().StartEffect(this));
        }
        if (currentSpell.GetSpellType() == Spell.Types.passiveSwitchable)
        {
            currentSpell.SwitchPassive();
            equipment.AddPassiveSpellsTo(stats);
        }
    }
    public void ApplyStatusEffect(StatusEffect effect)
    {
        stats.ApplyStatusEffect(effect);
    }
    public void DeapplyStatusEffect(StatusEffect effect)
    {
        stats.DeapplyStatusEffect(effect);
    }
    
    private void Die()
    {
        isDead = true;
        //healthRenderer.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = false;
        Destroy(transform.gameObject, 0.1f);
    }

    public Spell GetCurrentSpell() { return currentSpell; }
    public EntityStats GetStats() { return stats; }
    public float GetAttackRange() { return stats.GetAttackRange(); }
    public float GetAttackCooldown()
    {
        return stats.GetBaseAttackSpeed() * 100 / (stats.GetAttackSpeed());
    }
    public void SetCurrentSpell(int spellSlot)
    {
        currentSpell = equipment.GetSpell(spellSlot);
    }
}
