using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

//TODO: Change name of class
public class EffectSlot : MonoBehaviour
{
    [SerializeField] private Image m_MainImage;
    [SerializeField] private Image m_BorderImage;
    [SerializeField] private TextMeshProUGUI m_Text;
    private StatusEffect m_Effect;

    Color fullSlot = new Color(1, 1, 1, 1), emptySlot = new Color(1, 1, 1, 0);
    // public Slider slider;
    [HideInInspector] public float durationTime;
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

    public void SetEffect(StatusEffect effect)
    {
        m_Effect = effect;
        m_MainImage.color = new Color(1, 1, 1, 1);
        m_MainImage.sprite = effect.EffectSprite;
        effect.OnEffectEnd += (StatusEffect f) => { EmptySlot(); };
    }

    public void Update()
    {
        if (m_Effect == null)
        {
            m_MainImage.enabled = false;
            m_BorderImage.enabled = false;
            return;
        }
        m_MainImage.enabled = true;
        m_BorderImage.enabled = true;
        if (m_Effect.TryGetComponent<SE_CountComponent>(out SE_CountComponent countComponent))
        {
            m_Text.text = countComponent.CurrentCount.ToString();
        }
        if (m_Effect.TryGetComponent<SE_TimeComponent>(out SE_TimeComponent timeComponent))
        {
            m_Text.text = timeComponent.LeftTime.ToString();
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }

    }

    private void EmptySlot()
    {
        m_Text.text = "";
        m_MainImage.color = new Color(1, 1, 1, 0);
    }
}
