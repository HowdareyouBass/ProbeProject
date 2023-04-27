using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    [SerializeField] private GameEventListener gameEventListener = new GameEventListener();

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = Player.playerStats;

        gameEventListener.onEventTriggered += SetHealth;
        gameEventListener.gameEvent.AddListener(gameEventListener);

        slider.maxValue = playerStats.GetMaxHealth();
        SetHealth();
    }

    private void SetHealth()
    {
        slider.value = playerStats.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}