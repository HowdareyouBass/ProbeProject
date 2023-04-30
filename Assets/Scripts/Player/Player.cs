using System;
using UnityEngine;

//MonoBehaviour, 
public class Player : Entity
{
    public const float PLAYER_RADIUS = 1f;
    public GameEvents.PlayerEvents events;

    private PlayerEquipment equipment;

    public Player()
    {
        events = new GameEvents.PlayerEvents();
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

    public override GameEvent<float> GetOnDamageEvent() { return events.OnPlayerDamaged; }
    public override GameEvent GetOnDeathEvent() { return events.OnPlayerDeath; }
    public override GameEvent GetOnAttackEvent() { return events.OnPlayerAttack; } 
    public override Spell GetSpellFromSlot(int spellSlot) { return equipment.GetSpell(spellSlot); }
    public EntityStats GetStats() { return (EntityStats) stats; }
    public EntityEquipment GetEquipment() { return equipment; }
}
