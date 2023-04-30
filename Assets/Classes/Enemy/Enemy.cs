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
    //public Spell GetCurrentSpell() { return currentSpell; }
    //public void SetCurrentSpell(int spellSlot)
    //{
    //    currentSpell = equipment.GetSpell(spellSlot);
    //}
}
