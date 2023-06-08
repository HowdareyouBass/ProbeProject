using UnityEngine;

public class S_SelfCastSpell : S_ActiveSpellComponent
{
    protected override void Cast(Transform caster, Target target, Vector3 effectPosition)
    {
        base.Cast(caster, target, caster.position);
        
    }
}