using System.Reflection;
using UnityEngine;

public class S_GetDataFromEntityStatusEffectsComponent : SpellComponent
{
    [SerializeField] private StatusEffect m_EffectToGetDataFrom;
    [SerializeField] private PropertyInfo m_PropertyToGetDataFrom;

    [SerializeField] private StatusEffect m_EffectToSetDataTo;
    [SerializeField] private PropertyInfo m_PropertyToSetDataTo;

    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(GetAndSetData);
    }

    private void GetAndSetData()
    {
        m_PropertyToSetDataTo.SetValue(m_EffectToSetDataTo, m_PropertyToGetDataFrom.GetValue(m_EffectToGetDataFrom));
    }
}
