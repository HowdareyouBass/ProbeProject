using System;
using UnityEngine;
using System.Collections.Generic;

//MonoBehaviour, 
public class Player : Entity
{
    public const float PLAYER_RADIUS = 1f;

    private PlayerEquipment equipment;

    public Player()
    {
        equipment = new PlayerEquipment();
    }

    public void SetRace(Race race)
    {
        stats.ApplyRace(race);
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
    public override float GetAttackCooldown()
    { 
        return stats.GetBaseAttackSpeed() * 100 / (equipment.GetAttackSpeed() + stats.GetAttackSpeed());
    }
    public override Spell GetSpellFromSlot(int spellSlot) { return equipment.GetSpell(spellSlot); }
    public EntityStats GetStats() { return (EntityStats) stats; }
    public EntityEquipment GetEquipment() { return equipment; }
}
