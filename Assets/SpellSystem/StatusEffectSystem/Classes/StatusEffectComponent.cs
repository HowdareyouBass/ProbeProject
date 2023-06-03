using UnityEngine;

[System.Serializable]
public class StatusEffectComponent : CustomComponent
{
    [HideInInspector] public StatusEffect statusEffect;
    [HideInInspector] public Transform target;

    public virtual void Init()
    {
    }
}
