using UnityEngine;

public class TargetCastSpell : ActiveSpell
{
    [SerializeField] private int m_CastRange;
    public int castRange { get => m_CastRange / 100; }

    public override void Cast(Transform caster, Transform target)
    {
        GetComponent<SpellScript>().Init(caster, target);
        base.Cast(caster, target);
        GameObject spellGO = Instantiate(gameObject, caster.transform.position, Quaternion.identity);
        spellGO.GetComponent<SpellScript>().events.GetEvent(SpellEventName.OnCast).Trigger();
        if (effect != null)
            Instantiate(effect, spellGO.transform);
    }
}