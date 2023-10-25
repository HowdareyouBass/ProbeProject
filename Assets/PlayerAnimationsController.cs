using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerScript), typeof(Attack), typeof(NavMeshAgent))]
[RequireComponent(typeof(SpellCaster))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    private AnimatorOverrideController m_AnimatorOverride;

    private Attack m_AttackScript;
    private SpellCaster m_SpellCasterScript;
    private PlayerScript m_PlayerScript;
    private NavMeshAgent m_Agent;

    private int m_SpeedHash;
    private int m_SpellHash;
    private int m_CastSpellHash;
    private int m_AttackHash;
    private int m_AttackAnimationSpeedHash;
    
    private void Start()
    {
        m_AnimatorOverride = new AnimatorOverrideController(m_Animator.runtimeAnimatorController);
        m_Animator.runtimeAnimatorController = m_AnimatorOverride;
        
        m_SpeedHash = Animator.StringToHash("Speed");
        m_AttackHash = Animator.StringToHash("IsAttacking");
        // m_SpellCastHash = Animator.StringToHash("SpellCasted");
        m_AttackAnimationSpeedHash = Animator.StringToHash("AttackAnimationSpeed");
        m_SpellHash = Animator.StringToHash("SpellCasted");
        m_CastSpellHash = Animator.StringToHash("CastSpell");

        m_AttackScript = GetComponent<Attack>();
        m_SpellCasterScript = GetComponent<SpellCaster>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerScript = GetComponent<PlayerScript>();

        m_SpellCasterScript.OnSpellCast += ChangeSpellAnimation;
        m_AttackScript.StartAttackAnimation += StartAttackAnimation;
        m_AttackScript.StopAttackAnimation += StopAttackAnimation;
        m_PlayerScript.GetEntity().Events.GetEvent(EntityEventName.OnDeath).Subscribe(PlayDeathAnimation);
    }

    private void PlayDeathAnimation()
    {
        m_Animator.Play("Death");
    }
    private void StartAttackAnimation()
    {
        m_Animator.ResetTrigger("AttackInterrupted");
        m_Animator.SetTrigger("AttackPerformed");
    }

    private void StopAttackAnimation()
    {
        m_Animator.SetTrigger("AttackInterrupted");
    }

    private void ChangeSpellAnimation(Spell spell)
    {
        // If have no spell animation do nothing
        if (spell.SpellAnimation == null) return;
        m_AnimatorOverride["CastSleep"] = spell.SpellAnimation;
        m_Animator.runtimeAnimatorController = m_AnimatorOverride;
        m_Animator.SetTrigger(m_SpellHash);
        // m_Animator.Play("CastSpell");
    }

    private void Update()
    {
        m_Animator.SetFloat(m_SpeedHash, m_Agent.velocity.magnitude / m_Agent.speed);
        // m_Animator.SetBool(m_AttackHash, m_AttackScript.IsAttacking);
        m_Animator.SetFloat(m_AttackAnimationSpeedHash, m_PlayerScript.GetEntity().GetAttackCooldown() / 1.113f);// Animation duration
    }
}
