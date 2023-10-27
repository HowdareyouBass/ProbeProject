using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "Race")]
public class Race : ScriptableObject
{
    [field: SerializeField] public float Attack { get; private set; }
    [field: SerializeField] public int AttackRange { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] [field: Range(1f, 1.7f)] public float BaseAttackSpeed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float Stamina { get; private set; }
    [field: SerializeField] public float Defense { get; private set; }
    [field: SerializeField] public float CounterAttack { get; private set; }
    [field: SerializeField] public float ExperienceForKill { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float Evasion { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float MagicGift { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float PhysicsGift { get; private set; }
}