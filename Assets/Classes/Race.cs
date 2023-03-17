using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "Race")]
public class Race : ScriptableObject
{
    public float attackRange;
    public float health;
    [Range(0f, 1f)]
    public float evasion;
    [Range(0f, 1f)]
    public float counterAttack;
    public float attack;
    public float defense;
    public float magicGift;
    public float physicsGift;
}