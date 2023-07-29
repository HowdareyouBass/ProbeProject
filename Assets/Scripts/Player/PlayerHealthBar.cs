using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerScript m_PlayerScript;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Gradient m_Gradient;
    [SerializeField] private Image m_Fill;

    private Entity m_Player;
    private GameEvent<float> m_OnHealthChanged;

    private void Start()
    {
        m_Slider.maxValue = m_Player.stats.MaxHealth;
        SetHealthbarValue(m_Player.stats.MaxHealth);
    }
    private void OnEnable()
    {
        m_Player = m_PlayerScript.GetEntity();
        m_OnHealthChanged = m_Player.events.GetEvent<float>(EntityEventName.OnHealthChanged, true);
        m_OnHealthChanged?.Subscribe(ChangeHealthbarValue);
    }
    private void OnDisable()
    {
        m_OnHealthChanged?.Unsubscribe(ChangeHealthbarValue);
    }

    private void ChangeHealthbarValue(float value)
    {
        SetHealthbarValue(m_Slider.value - value);
    }
    private void SetHealthbarValue(float value)
    {
        m_Slider.value = value;
        m_Fill.color = m_Gradient.Evaluate(m_Slider.normalizedValue);
    }
}