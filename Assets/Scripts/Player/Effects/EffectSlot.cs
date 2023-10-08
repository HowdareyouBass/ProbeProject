using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlot : MonoBehaviour
{
    public Image image;
    Color fullSlot = new Color(1, 1, 1, 1), emptySlot = new Color(1, 1, 1, 0);
    // public Slider slider;
    [HideInInspector]public float durationTime;
   /* [HideInInspector] */ public float currentTime;


    public void InitializeNewTime(SpellStats newEffect)
    {
        durationTime = (float)newEffect.durationTime;
        currentTime = durationTime;
    }

    public void SetTime(float time)
    {
        currentTime = time;
    }

    public void Update()
    {
        if (transform.childCount == 0)
        {
            image.color = emptySlot;
        }
        else
        {
            image.color = fullSlot;
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }

    }
}
