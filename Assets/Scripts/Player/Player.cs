using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IEntity
{
    [SerializeField] private Race playerRace;

    public const float PLAYER_RADIUS = 1f;
    public PlayerEvents events = new PlayerEvents();

    private PlayerStats stats;
    private PlayerEquipment equipment;

    void Awake()
    {
        stats = new PlayerStats();     
        equipment = new PlayerEquipment();
        stats.ApplyRace(playerRace);
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

    public float GetAttackDamage() { return stats.GetAttackDamage(); }
    public float GetAttackCooldown()
    { 
        return stats.GetBaseAttackSpeed() * 100 / (equipment.GetAttackSpeed() + stats.GetAttackSpeed());
    }

    public GameEvent<float> GetOnDamageEvent() { return events.OnPlayerDamaged; }
    public GameEvent GetOnDeathEvent() { return events.OnPlayerDeath; }
    public Spell GetSpellFromSlot(int spellSlot) { return equipment.GetSpell(spellSlot); }
    public float GetMaxHealth() { return stats.GetMaxHealth(); }
    public EntityStats GetStats() { return stats; }
    public EntityEquipment GetEquipment() { return equipment; }
}
