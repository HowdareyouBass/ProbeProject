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

    public virtual void Init() { Debug.Log("Called base Init method for Spell component"); }
    public virtual void Destroy() { Debug.Log("Called base Destroy method for Spell component"); }
}