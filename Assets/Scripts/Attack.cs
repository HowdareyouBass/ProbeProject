using System;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Movement m_Movement;
    private Entity m_Entity;
    private EntityController m_Controller;
    private Coroutine m_Attacking;
    private Coroutine m_DelayedAttack;
    private bool m_AttackNotOnCooldown = true;
    private Target m_CurrentTarget;

    public event Action StartAttackAnimation;
    public event Action StopAttackAnimation;

    // Some problem with coroutines cuz if you use StopCoroutine function the coroutine that stopped doesn't become null
   //  public bool IsAttacking { get; private set; }

    private void Start()
    {
        m_Movement = GetComponent<Movement>();
        m_Controller = GetComponent<EntityController>();
        m_Entity = GetComponent<EntityScript>().GetEntity();
    }

    public void Stop()
    {
        m_CurrentTarget = null;
        StopAttackAnimation.Invoke();
        if (m_Attacking != null)
        {
            StopCoroutine(m_Attacking);
        }
        if (m_DelayedAttack != null)
        {
            StopCoroutine(m_DelayedAttack);
        }
    }
    
    public void AttackTarget(Target target)
    {
        //Already attacking
        if (target.transform == m_CurrentTarget?.transform) return;
        m_Controller.StopActions();
        m_CurrentTarget = target;
        m_Attacking = StartCoroutine(AttackTargetRoutine());
    }

    private IEnumerator AttackTargetRoutine()
    {
        while (true)
        {
            yield return m_Movement.FolowUntilInRange(m_CurrentTarget, m_Entity.Stats.AttackRange);

            if (m_AttackNotOnCooldown && m_Entity.CanAttack)
            {
                // IsAttacking = true;
                StartAttackAnimation.Invoke();
                m_DelayedAttack = StartCoroutine(DelayedAttackTarget());
                // m_Entity.AttackTarget(target);
                StartCoroutine(WaitForNextAttack());
            }

            //Stops m_Attacking enemy that already died
            if(m_CurrentTarget.transform.GetComponent<EntityScript>().isDead)
            {
                // IsAttacking = false;
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator DelayedAttackTarget()
    {
        yield return new WaitForSeconds(0.3f);
        m_Entity.AttackTarget(m_CurrentTarget);
        yield break;
    }

    private IEnumerator WaitForNextAttack()
    {
        m_AttackNotOnCooldown = false;
        yield return new WaitForSeconds(m_Entity.GetAttackCooldown());
        m_AttackNotOnCooldown = true;
        yield break;
    }
}
