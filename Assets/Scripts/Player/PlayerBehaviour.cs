using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Race race;
    private PlayerEquipment playerEquipment;
    private PlayerStats playerStats;

    void Start()
    {
        //Start stats from race
        playerStats = new PlayerStats(race);
        playerEquipment = new PlayerEquipment();
    }

    public void EquipItem(Item item)
    {
        playerEquipment.EquipItem(item);
    }
    public void EquipSpell(Spell spell, int spellSlot)
    {
        playerEquipment.EquipSpell(spell, spellSlot);
    }

    public void AttackTarget(RaycastHit target)
    {
        target.transform.GetComponent<EnemyBehavior>().Damage(playerStats.GetAttackDamage());
    }

    public void CastSpell(int spellSlot)
    {
        Spell spell = playerEquipment.GetSpell(spellSlot);

        if (spell == null)
        {
            Debug.LogWarning("No spell in a slot " + (spellSlot + 1).ToString());
            return;
        }

        if (spell.GetSpellType() == Spell.Types.none)
        {
            Debug.LogWarning("Spell Type is none");
            return;
        }
        

        //Don't ask me about that please
        //spell.GetEffect().GetComponent<Projectile>().spell = spell;
        GameObject castEffect = Instantiate(spell.GetEffect(), transform.position, transform.rotation);

        if (spell.GetSpellType() == Spell.Types.projectile)
        {
            Rigidbody rb = castEffect.AddComponent<Rigidbody>();
            rb.useGravity = false;

            SphereCollider collider = castEffect.AddComponent<SphereCollider>();
            collider.radius = 0.4f;
            collider.isTrigger = true;

            Projectile spellProjectileComponent = castEffect.AddComponent<Projectile>();
            spellProjectileComponent.spell = spell;
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
        return playerStats.GetBaseAttackSpeed() * 100 / (playerEquipment.GetAttackSpeed() + 20);
    }
}
