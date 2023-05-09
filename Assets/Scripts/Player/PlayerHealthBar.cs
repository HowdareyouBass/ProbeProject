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
    private GameEvent<float> m_OnDamaged;

    private void Start()
    {
        m_Slider.maxValue = m_Player.stats.maxHealth;
        SetHealthbarValue(m_Player.stats.maxHealth);
    }
    private void OnEnable()
    {
        m_Player = m_PlayerScript.GetEntity();
        m_OnDamaged = m_Player.GetEvent<float>(EntityEventName.OnDamaged, true);
        m_OnDamaged?.Subscribe(DecreaseHealthbarValue);
    }
    private void OnDisable()
    {
        m_OnDamaged?.Unsubscribe(DecreaseHealthbarValue);
    }

    private void DecreaseHealthbarValue(float value)
    {
        if (value < 0)
            throw new ArgumentException("Damage can not be negative.");
        SetHealthbarValue(m_Slider.value - value);
    }
    private void SetHealthbarValue(float value)
    {
        m_Slider.value = value;
        m_Fill.color = m_Gradient.Evaluate(m_Slider.normalizedValue);
    }
}