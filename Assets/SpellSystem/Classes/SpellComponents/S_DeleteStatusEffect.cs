using UnityEngine;

public class S_DeleteStatusEffect : SpellComponent
{
    [SerializeField] private StatusEffect m_EffectToDelete;

    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(DeleteStatusEffect);
    }

    private void DeleteStatusEffect()
    {
        caster.GetComponent<StatusEffectHandler>().DeleteStatusEffect(m_EffectToDelete);
    }
}