using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    //[SerializeField] private GameEventListener onPlayerDamaged = new GameEventListener();
    private Player player;
    void Start()
    {
        player = GameObject.Find("/Player").GetComponent<Player>();
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