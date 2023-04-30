using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    private Player player;
    private GameEvent<float> OnDamaged;
    //[SerializeField] private GameEventListener onPlayerDamaged = new GameEventListener();
    private void Awake()
    {
        
    }
    private void Start()
    {
        slider.maxValue = player.GetMaxHealth();
        SetHealthbarValue(player.GetMaxHealth());
    }
    private void OnEnable()
    {
        player = playerScript.GetPlayer();
        OnDamaged = player.GetEvents()[EventName.OnDamaged] as GameEvent<float>;
        OnDamaged?.Subscribe(DecreaseHealthbarValue);
    }
    private void OnDisable()
    {
        OnDamaged?.Unsubscribe(DecreaseHealthbarValue);
    }

    private void DecreaseHealthbarValue(float value)
    {
        if (value < 0)
            throw new ArgumentException("Damage can not be negative.");
        SetHealthbarValue(slider.value - value);
    }

    private void SetHealthbarValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}