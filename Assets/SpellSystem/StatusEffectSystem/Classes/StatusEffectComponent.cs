using UnityEngine;

[System.Serializable]
public abstract class StatusEffectComponent : CustomComponent
{
    [HideInInspector] public StatusEffect statusEffect;
    [HideInInspector] public Transform target;
    [HideInInspector] public Entity targetEntity { get => target.GetComponent<EntityScript>().GetEntity(); }

    //TODO: may be changename???
    public abstract void Init();
    //TODO: may be changename???
    public abstract void Destroy();
}
