using UnityEngine;

[System.Serializable]
public class Enemy : Entity
{
    private EnemyEquipment equipment;
    [HideInInspector] public bool isDead;

    public void SetRace(Race race)
    {
        stats.ApplyRace(race);
    }

    public override void ApplyAllPassiveSpells()
    {
        equipment.AddPassiveSpellsTo(stats);
    }
    public override Spell GetSpellFromSlot(int spellSlot) { return equipment.GetSpell(spellSlot); }
}
