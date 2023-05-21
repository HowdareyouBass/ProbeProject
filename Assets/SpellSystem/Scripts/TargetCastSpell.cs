using UnityEngine;

public class TargetCastSpell : ActiveSpell
{
    [SerializeField] private int m_CastRange;
    public int castRange { get => m_CastRange / 100; }

    public override void Cast(Transform caster, Transform target)
    {
        base.Cast(caster, target);
        GameObject spellGO = Instantiate(gameObject, caster.transform.position, Quaternion.identity);
        spellGO.GetComponent<SpellScript>().events.GetEvent(SpellEventName.OnCast).Trigger();
        if (m_Effect != null)
            Instantiate(m_Effect, spellGO.transform);
    }
}