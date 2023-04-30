using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System;

public class Enemy : Entity
{
    private EnemyEquipment equipment;
    public GameEvents.EnemyEvents events { get; private set; }
    [HideInInspector] public bool isDead;

    public Enemy()
    {
        events = new GameEvents.EnemyEvents();
    }

    public void SetRace(Race race)
    {
        stats.ApplyRace(race);
    }

    public override GameEvent<float> GetOnDamageEvent() { return events.OnEnemyDamaged; }
    public override GameEvent GetOnDeathEvent() { return events.OnEnemyDeath; }

    public override void ApplyAllPassiveSpells()
    {
        equipment.AddPassiveSpellsTo(stats);
    }
    public override Spell GetSpellFromSlot(int spellSlot) { return equipment.GetSpell(spellSlot); }
    //public Spell GetCurrentSpell() { return currentSpell; }
    //public void SetCurrentSpell(int spellSlot)
    //{
    //    currentSpell = equipment.GetSpell(spellSlot);
    //}
}
