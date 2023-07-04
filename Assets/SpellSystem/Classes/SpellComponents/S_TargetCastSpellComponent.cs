using UnityEngine;

[System.Serializable]
public class S_TargetCastSpellComponent : S_ActiveSpellComponent
{
    [SerializeField] private int m_CastRange;

    public int castRange { get => m_CastRange / 100; }

    protected override void Cast(Transform caster, Target target)
    {
        if (target.isEntity) base.Cast(caster, target);
    }
}