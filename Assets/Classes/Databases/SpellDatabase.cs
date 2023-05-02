using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Database", menuName = "SpellDatabase")]
public class SpellDatabase : ScriptableObject
{
    public List<Spell1> spells = new List<Spell1>();
}