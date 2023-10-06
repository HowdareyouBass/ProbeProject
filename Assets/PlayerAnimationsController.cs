using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerScript), typeof(Attack), typeof(NavMeshAgent))]
[RequireComponent(typeof(SpellCaster))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    private Attack m_AttackScript;
    private SpellCaster m_SpellCasterScript;
    private PlayerScript m_PlayerScript;
    private NavMeshAgent m_Agent;

    private int m_SpeedHash;
    private int m_AttackHash;
    private int m_AttackAnimationSpeedHash;
    
    private void Start()
    {
        m_SpeedHash = Animator.StringToHash("Speed");
        m_AttackHash = Animator.StringToHash("IsAttacking");
        // m_SpellCastHash = Animator.StringToHash("SpellCasted");
        m_AttackAnimationSpeedHash = Animator.StringToHash("AttackAnimationSpeed");
        m_AttackScript = GetComponent<Attack>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        m_Animator.SetFloat(m_SpeedHash, m_Agent.velocity.magnitude / m_Agent.speed);
        m_Animator.SetBool(m_AttackHash, m_AttackScript.IsAttacking);
        m_Animator.SetFloat(m_AttackAnimationSpeedHash, m_PlayerScript.GetEntity().GetAttackCooldown() * 1.113f);
    }
}
