using UnityEngine;

[RequireComponent(typeof(SpellScript))]
public class SelfCastSpell : ActiveSpell
{
    protected override void Cast(Transform caster, Transform target)
    {
        GetComponent<SpellScript>().Init(caster, null);
        base.Cast(caster, target);
        spellScript.events.GetEvent(SpellEventName.OnCast).Trigger();
        if (effect != null)
            Instantiate(effect, transform);
    }
}