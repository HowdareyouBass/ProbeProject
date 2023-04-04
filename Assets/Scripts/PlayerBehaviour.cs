using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Race race;
    public PlayerEquipment playerEquipment;
    public GameObject go;
    private PlayerStats playerStats;

    void Start()
    {
        //Start stats from race
        playerStats = new PlayerStats(race);
        playerEquipment = new PlayerEquipment();
        playerEquipment.EquipItem(new Item(300));
        playerEquipment.EquipSpell(new ProjectileSpell(go), 0);
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
        return playerStats.GetBaseAttackSpeed() / (playerEquipment.GetAttackSpeed() * 100 + 1);
    }
}
