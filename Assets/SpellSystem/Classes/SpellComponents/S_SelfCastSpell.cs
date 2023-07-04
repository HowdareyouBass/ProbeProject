using UnityEngine;

public class S_SelfCastSpell : S_ActiveSpellComponent
{
    protected override void Cast(Transform caster, Target target)
    {
        effectPosition = caster.position;
        base.Cast(caster, target);
        
    }
}