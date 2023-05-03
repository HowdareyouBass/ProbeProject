using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Debug Settings", menuName = "DebugSettings")]
public class DebugSettings : ScriptableObject
{
    [HideInInspector]public PlayerBehaviour player;
    public SpellDatabase spellDatabase;
    public ItemDatabase itemDatabase;
}
