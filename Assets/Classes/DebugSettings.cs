using UnityEngine;

[CreateAssetMenu(fileName = "New Debug Settings", menuName = "DebugSettings")]
public class DebugSettings : ScriptableObject
{
    public ItemDatabase itemDatabase;
    public Player player;
}