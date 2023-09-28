using System;
using UnityEngine;

[Serializable]
public class SpellComponent : CustomComponent
{
    [HideInInspector] public Spell spell;
    [HideInInspector] public Transform caster;
    [HideInInspector] public Target target;

    public Entity casterEntity => caster.GetComponent<EntityScript>().GetEntity();
    public Entity targetEntity => caster.GetComponent<EntityScript>().GetEntity();

    public EntityStats casterStats => casterEntity.Stats;
    public EntityStats targetStats => targetEntity.Stats;

    public virtual void Init() {}
    public virtual void LateInit() {}
    public virtual void Destroy() {}
    public virtual void Disable() {}
    public virtual void Enable() {}
}