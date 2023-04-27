using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IEntity
{
    public const float PLAYER_RADIUS = 1f;

    public static Player instance = null;
    public static PlayerStats stats { get; private set; }

    [SerializeField] private Race race;
    
    public GameEvent OnPlayerDamaged;

    private PlayerEquipment equipment;
    private Spell currentSpell;
    //public bool isCastingSpell = false;

    void Awake()
    {
        stats = new PlayerStats();
        stats.ApplyRace(race);
        equipment = new PlayerEquipment();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : MORE THEN 1 PLAYER BEHAVIOUR COMPONENT IS NOT ALOUD!");
        }
    }

    public void Damage(float amount)
    {
        //stats.Damage(amount);
        OnPlayerDamaged.TriggerEvent();
    }

    public void EquipItem(Item item)
    {
        equipment.EquipItem(item);
    }
    public void EquipSpell(Spell spell, int spellSlot)
    {
        equipment.EquipSpell(spell, spellSlot);
        equipment.AddPassiveSpellsTo(stats);
    }

    public void DamageTarget(RaycastHit target)
    {
        target.transform.GetComponent<Health>().Damage(stats.GetAttackDamage());
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

    public float GetAttackRange() { return stats.GetAttackRange(); }
    public float GetAttackDamage() { return stats.GetAttackDamage(); }
    public float GetAttackCooldown()
    { 
        return stats.GetBaseAttackSpeed() * 100 / (equipment.GetAttackSpeed() + stats.GetAttackSpeed());
    }

    public Spell GetCurrentSpell() { return currentSpell; }
    public float GetMaxHealth() { return stats.GetMaxHealth(); }
    public EntityStats GetStats() { return stats; }

    public void SetCurrentSpell(int spellSlot)
    {
        currentSpell = equipment.GetSpell(spellSlot);
    }
}
