using UnityEngine;

[System.Serializable]
public abstract class StatusEffectComponent : CustomComponent
{
    [HideInInspector] public StatusEffect statusEffect;
    [HideInInspector] public Transform target;
    [HideInInspector] public Entity targetEntity { get => target.GetComponent<EntityScript>().GetEntity(); }

    public abstract void Init();
    public abstract void Destroy();
}
