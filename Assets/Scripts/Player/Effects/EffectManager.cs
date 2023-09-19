using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public EffectSlot[] effectSlots;
    public GameObject effectItemPrefab;

    private void Update()
    {
        for (int i = 0; i < effectSlots.Length - 1; ++i)
        {
            EffectSlot slot = effectSlots[i + 1];
            EffectItem effectInSlot = slot.GetComponentInChildren<EffectItem>();
           
            EffectSlot prevSlot = effectSlots[i];   
            EffectItem prevEffectInSlot = prevSlot.GetComponentInChildren<EffectItem>();

            if (!prevEffectInSlot && effectInSlot)
            {
                SpawnNewEffect(effectInSlot.effect, prevSlot);
                prevSlot.SetTime(slot.currentTime);

                Destroy(effectInSlot.gameObject);
            }
        }

        for (int i = 0; i < effectSlots.Length; ++i)
        {
            EffectSlot slot = effectSlots[i];
            EffectItem effectInSlot = slot.GetComponentInChildren<EffectItem>();

            if (slot.currentTime < 0 && effectInSlot)
            {
                Destroy(effectInSlot.gameObject);
            }
        }
    }
    public bool AddEffect(SpellStats effect)
    {
        for (int i = 0; i < effectSlots.Length; ++i)
        {
            EffectSlot slot = effectSlots[i];
            EffectItem effectInSlot = slot.GetComponentInChildren<EffectItem>();

            if (!effectInSlot)
            {
                SpawnNewEffect(effect, slot);
                return true;
            }
            else if(effectInSlot.effect == effect)
            {
                slot.InitializeNewTime(effect);
                return true;
            }
        }

        return false;
    }

    void SpawnNewEffect(SpellStats effect, EffectSlot slot)
    {
        GameObject newEffectGo = Instantiate(effectItemPrefab, slot.transform);
        EffectItem effectItem = newEffectGo.GetComponent<EffectItem>();
        effectItem.InitializeEffect(effect);
        slot.InitializeNewTime(effect);
    }
}
