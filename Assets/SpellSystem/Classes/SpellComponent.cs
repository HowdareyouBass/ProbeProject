using System;
using UnityEngine;

//TODO: Do destroy method to unsubsribe from events
[Serializable]
public abstract class SpellComponent : CustomComponent
{
    [HideInInspector] public Spell spell;
    [HideInInspector] public Transform caster;
    [HideInInspector] public Target target;
    public Entity casterEntity => caster.GetComponent<EntityScript>().GetEntity();

    public abstract void Init();
}