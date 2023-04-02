using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Race race;
    public PlayerEquipment playerItems;
    private PlayerStats playerStats;

    void Start()
    {
        //Start stats from race
        playerStats = new PlayerStats(race);
        playerItems = new PlayerEquipment();
        playerItems.EquipItem(new Item(300));
    }

    public void AttackTarget(RaycastHit target)
    {
        target.transform.GetComponent<EnemyBehavior>().Damage(playerStats.GetAttackDamage());
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
        return playerStats.GetBaseAttackSpeed() / playerItems.GetAttackSpeed() * 100;
    }
}
