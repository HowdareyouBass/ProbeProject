using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items = new List<Item>();
}