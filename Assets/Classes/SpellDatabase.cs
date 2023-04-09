using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Database", menuName = "SpellDatabase")]
public class SpellDatabase : ScriptableObject
{
    public List<Spell> spells = new List<Spell>();
}