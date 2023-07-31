using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEffectScript : MonoBehaviour
{
    public EffectManager effectManager;
    public SpellStats[] spellsToInitialize;

    public void AddSpell(int id)
    {
        bool result = effectManager.AddEffect(spellsToInitialize[id]);
        if (result)
        {
            Debug.Log("Effect added");
        }
        else
        {
            Debug.Log("Effect not added");
        }
    }
}
