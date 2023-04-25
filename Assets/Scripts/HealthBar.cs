using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;


    [SerializeField] private PlayerBehaviour player;
    [SerializeField] private GameEventListener gameEventListener = new GameEventListener();

    void OnEnable()
    {
        gameEventListener.onEventTriggered.AddListener(SetHealth);
        gameEventListener.gameEvent.AddListener(gameEventListener);

        slider.maxValue = player.GetMaxHealth();
        slider.value = player.GetCurrentHealth();
        fill.color = gradient.Evaluate(1f);
    }

    private void SetHealth()
    {
        slider.value = player.GetCurrentHealth();

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}