using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Race race;
    public PlayerEquipment playerEquipment;
    public GameObject go;
    public Spell so;
    private PlayerStats playerStats;

    void Start()
    {
        //Start stats from race
        playerStats = new PlayerStats(race);

        playerEquipment = new PlayerEquipment();
        playerEquipment.EquipItem(new Item(0));

        go.GetComponent<Projectile>().spellSlot = 0;
        go.GetComponent<Projectile>().spell = so;
        ProjectileSpell fireball = new ProjectileSpell(go);
        fireball.SetSpellStats(so);
        playerEquipment.EquipSpell(fireball, 0);
    }

    public void AttackTarget(RaycastHit target)
    {
        target.transform.GetComponent<EnemyBehavior>().Damage(playerStats.GetAttackDamage());
    }

    public void CastSpell(int spellSlot)
    {
        GameObject castEffect = Instantiate(playerEquipment.GetSpell(spellSlot).CastEffect(), transform.position, transform.rotation);
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
        return playerStats.GetBaseAttackSpeed() * 100 / (playerEquipment.GetAttackSpeed() + 100);
    }
}