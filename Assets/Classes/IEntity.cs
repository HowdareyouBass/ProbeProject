using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    float GetAttackRange();
    float GetAttackCooldown();
    Spell GetCurrentSpell();
    EntityStats GetStats();
    void ApplyStatusEffect(StatusEffect effect);
    void DeapplyStatusEffect(StatusEffect effect);
    void SetCurrentSpell(int spellSlot);
    void CastSpell(RaycastHit target);
    void DamageTarget(RaycastHit target)
    {
        target.transform.GetComponent<Health>().Damage(GetStats().GetAttackDamage());
    }
    
}
