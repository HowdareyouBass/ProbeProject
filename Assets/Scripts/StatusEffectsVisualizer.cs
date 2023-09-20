using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsVisualizer : MonoBehaviour
{
    [SerializeField] private StatusEffectHandler m_Handler;
    private EffectSlot[] m_EffectSlots;
    private int m_Iterator = 0;

    private void Start()
    {
        m_EffectSlots = transform.GetComponentsInChildren<EffectSlot>();
        m_Handler.OnEffectAdded += ObserveEffect;
    }

    private void ObserveEffect(StatusEffect effect)
    {
        if (m_Iterator > m_EffectSlots.Length)
        {
            Debug.Log("Not enough slots in effects");
            return;
        }
        m_EffectSlots[m_Iterator].SetEffect(effect);
    }
}