using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Derive stamina bar and Stamina bar from some parent object
public class StaminaGuiBar : MonoBehaviour
{
    [SerializeField] private PlayerScript m_PlayerScript;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Gradient m_Gradient;
    [SerializeField] private Image m_Fill;

    private Entity m_Player;
    private GameEvent<float> m_OnStaminaChanged;

    private void Start()
    {
        m_Slider.maxValue = m_Player.stats.MaxStamina;
        SetStaminabarValue(m_Player.stats.MaxStamina);
    }
    private void OnEnable()
    {
        m_Player = m_PlayerScript.GetEntity();
        m_OnStaminaChanged = m_Player.events.GetEvent<float>(EntityEventName.OnStaminaChanged, true);
        m_OnStaminaChanged?.Subscribe(ChangeStaminabarValue);
    }
    private void OnDisable()
    {
        m_OnStaminaChanged?.Unsubscribe(ChangeStaminabarValue);
    }

    private void ChangeStaminabarValue(float value)
    {
        SetStaminabarValue(m_Slider.value - value);
    }
    private void SetStaminabarValue(float value)
    {
        m_Slider.value = value;
        m_Fill.color = m_Gradient.Evaluate(m_Slider.normalizedValue);
    }
}