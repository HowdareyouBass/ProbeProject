using System;
using UnityEngine;

[Serializable]
public class SpellComponent : CustomComponent
{
    [HideInInspector] public Spell spell;
    [HideInInspector] public Transform caster;
    [HideInInspector] public Target target;
    public Entity casterEntity => caster.GetComponent<EntityScript>().GetEntity();
}