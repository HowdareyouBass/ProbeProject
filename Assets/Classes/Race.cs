using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "Race")]
public class Race : ScriptableObject
{
    public float attack;
    public float attackRange;
    public float attackSpeed;
    [Range(1f, 1.7f)]
    public float baseAttackSpeed;
    public float health;
    public float defense;
    public float counterAttack;
    [Range(0f, 1f)]
    public float evasion;
    [Range(0f, 1f)]
    public float magicGift;
    public float physicsGift;
}