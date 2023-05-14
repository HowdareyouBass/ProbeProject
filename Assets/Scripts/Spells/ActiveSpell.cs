using UnityEngine;

public class ActiveSpell : SpellComponent, ICastable
{
    private enum EffectPlacement { Caster, Target };
    [SerializeField] private int m_CastRange;
    [SerializeField] private GameObject m_Effect;
    [SerializeField] private EffectPlacement m_EffectPlacement;

    public int castRange { get => m_CastRange / 100; }

    public void Cast(Transform caster, Transform target)
    {
        GetComponent<SpellScript>().Init(caster, target);
        Vector3 spellStartPosition = Vector3.zero;

        if (m_EffectPlacement == EffectPlacement.Caster)
            spellStartPosition = caster.position;
        if (m_EffectPlacement == EffectPlacement.Target)
            spellStartPosition = target.position;
        
        GameObject spellGO = Instantiate(gameObject, spellStartPosition, Quaternion.identity);
        spellGO.GetComponent<SpellScript>().events.GetEvent(SpellEventName.OnCast).Trigger();
        if (m_Effect != null)
            Instantiate(m_Effect, spellGO.transform);
    }
}