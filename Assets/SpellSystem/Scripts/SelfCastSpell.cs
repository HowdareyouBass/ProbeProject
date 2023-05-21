using UnityEngine;

[RequireComponent(typeof(SpellScript))]
public class SelfCastSpell : ActiveSpell
{
    private void Awake()
    {
        GetComponent<SpellScript>().Init(transform.parent, null);
    }

    public override void Cast(Transform caster, Transform target)
    {
        base.Cast(caster, target);
        spellScript.events.GetEvent(SpellEventName.OnCast).Trigger();
        if (m_Effect != null)
            Instantiate(m_Effect, transform);
    }
}