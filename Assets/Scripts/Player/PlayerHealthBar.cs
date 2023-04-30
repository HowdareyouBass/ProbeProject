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
    //[SerializeField] private GameEventListener onPlayerDamaged = new GameEventListener();
    void Start()
    {
        player = playerScript.GetPlayer();
        player.events.OnPlayerDamaged.Subscribe(DecreaseHealthbarValue);
        slider.maxValue = player.GetMaxHealth();

        SetHealthbarValue(player.GetMaxHealth());
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