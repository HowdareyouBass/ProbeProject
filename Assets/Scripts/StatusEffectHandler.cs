using UnityEngine;
using System.Collections.Generic;

public class StatusEffectHandler : MonoBehaviour
{
    //TODO: nullify comments
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
    }
    private void RemoveEffect(StatusEffect effect)
    {
        m_StatusEffects.Remove(effect);
    }
}
