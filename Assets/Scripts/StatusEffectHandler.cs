using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class StatusEffectHandler : MonoBehaviour
{
    public ReadOnlyCollection<StatusEffect> Effects => m_StatusEffects.AsReadOnly();
    private List<StatusEffect> m_StatusEffects;

    private void Awake()
    {
        m_StatusEffects = new List<StatusEffect>(50);
    }

    public void AddEffect(StatusEffect effect)
    {
        effect.Init(transform);
        effect.OnEffectEnd += RemoveEffect;
        m_StatusEffects.Add(effect);
        effect.StartEffect();
    }
    private void RemoveEffect(StatusEffect effect)
    {
        m_StatusEffects.Remove(effect);
        Destroy(effect);
    }
}
