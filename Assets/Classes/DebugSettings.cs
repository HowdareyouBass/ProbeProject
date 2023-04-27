using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Debug Settings", menuName = "DebugSettings")]
public class DebugSettings : ScriptableObject
{
    public SpellDatabase spellDatabase;
    public ItemDatabase itemDatabase;
    public Player player;
}
