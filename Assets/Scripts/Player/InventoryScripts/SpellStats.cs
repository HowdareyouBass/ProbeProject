using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Spell")]
public class SpellStats : ScriptableObject
{
    [Header("OnlyGameplay")]
    public TileBase tile;
    public SpellType type;
    public EffectType effect;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public float durationTime = 0;

    [Header("Both")]
    public Sprite image;
}

public enum SpellType
{
    Buff,
    Debuff,
    Passive
}

public enum EffectType
{
    Health,
    Stamina,
    Walkspeed,
    Strength
}

