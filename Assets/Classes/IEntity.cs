using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    GameEvent GetOnDeathEvent();
    GameEvent<float> GetOnDamageEvent();
    float GetAttackRange() { return GetStats().GetAttackRange(); }
    float GetAttackDamage() { return GetStats().GetAttackDamage(); }
    float GetAttackCooldown();
    float GetMaxHealth() { return GetStats().GetMaxHealth(); }
    EntityStats GetStats();
    EntityEquipment GetEquipment();
    void ApplyStatusEffect(StatusEffect effect)
    {
        GetStats().ApplyStatusEffect(effect);
    }
    void DeapplyStatusEffect(StatusEffect effect)
    {
        GetStats().DeapplyStatusEffect(effect);
    }
    Spell GetSpellFromSlot(int spellSlot);
    //Spell GetCurrentSpell();
    //void SetCurrentSpell(int spellSlot);
    //void CastSpell(RaycastHit target);
    void DamageTarget(RaycastHit target)
    {
        target.transform.GetComponent<Health>().Damage(GetStats().GetAttackDamage());
    }
    
}
