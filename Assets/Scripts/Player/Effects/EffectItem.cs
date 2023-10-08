using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EffectItem : MonoBehaviour
{
    [HideInInspector]public SpellStats effect;
    [HideInInspector] public float durationTimeLeft;

    [Header("UI")]
    public Image image;

    public void InitializeEffect(SpellStats newEffect)
    {
        effect = newEffect;
        image.sprite = newEffect.image;
    }
}
