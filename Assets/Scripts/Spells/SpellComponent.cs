using UnityEngine;

public abstract class SpellComponent : MonoBehaviour
{
    [HideInInspector] public SpellScript spellScript;
    [HideInInspector] public Transform caster;
    [HideInInspector] public Transform target;
}