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
        GameObject castEffect = Instantiate(playerEquipment.GetSpell(spellSlot).GetEffect(), transform.position, transform.rotation);
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
