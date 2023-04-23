using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Race race;
    private PlayerEquipment playerEquipment;
    private PlayerStats playerStats;
    private Spell currentSpell;
    //public bool isCastingSpell = false;

    void Start()
    {
        //Start stats from race
        playerStats = new PlayerStats(race);
        playerEquipment = new PlayerEquipment();//d
    }

    public void EquipItem(Item item)
    {
        playerEquipment.EquipItem(item);
    }
    public void EquipSpell(Spell spell, int spellSlot)
    {
        playerEquipment.EquipSpell(spell, spellSlot);
        playerStats = playerEquipment.AddPassiveSpellsTo(playerStats);
    }

    public void AttackTarget(RaycastHit target)
    {
        target.transform.GetComponent<EnemyBehavior>().Damage(playerStats.GetAttackDamage());
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
            target.transform.GetComponent<EnemyBehavior>().Damage(currentSpell.GetDamage());
        }
        if (currentSpell.GetSpellType() == Spell.Types.directedAtGround)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffectOnImpact(), target.point, Quaternion.identity);
            Explosion spellExplosionComponent = castEffect.AddComponent<Explosion>();
            spellExplosionComponent.spell = currentSpell;
        }
    }

    public float GetAttackRange()
    {
        return playerStats.GetAttackRange();
    }
    public float GetAttackDamage()
    {
        return playerStats.GetAttackDamage();
    }
    public float GetAttackCooldown()
    {
        return playerStats.GetBaseAttackSpeed() * 100 / (playerEquipment.GetAttackSpeed() + playerStats.GetAttackSpeed());
    }

    public Spell GetCurrentSpell()
    {
        return currentSpell;
    }

    public void SetCurrentSpell(int spellSlot)
    {
        currentSpell = playerEquipment.GetSpell(spellSlot);
    }
}
