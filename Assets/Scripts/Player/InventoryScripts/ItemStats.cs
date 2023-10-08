using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class ItemStats : ScriptableObject
{ 
    [Header("OnlyGameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public int cost;

    [Header("Only UI")]
    public bool Stackable = false;

    [Header("Both")]
    public Sprite image;
}
public enum ItemType
{
    Tool,
    Weapon,
    Armour,
    CraftingItem
}

public enum ActionType
{
    Attack,
    Defend,
    Cast,
    CraftingItem
}