using System;
using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Movement m_Movement;
    private Entity m_Entity;
    private EntityController m_Controller;
    private Coroutine m_Attacking;
    private bool m_CanAttack = true;

    // public event Action OnAttackPerformed; 

    // Some problem with coroutines cuz if you use StopCoroutine function the coroutine that stopped doesn't become null
    public bool IsAttacking;

    private void Start()
    {
        IsAttacking = false;
        m_Movement = GetComponent<Movement>();
        m_Controller = GetComponent<EntityController>();
        m_Entity = GetComponent<EntityScript>().GetEntity();
    }

    public void Stop()
    { 
        if (m_Attacking != null)
        {
            IsAttacking = false;
            StopCoroutine(m_Attacking);
        }
    }
    
    public void AttackTarget(Target target)
    {
        m_Controller.StopActions();
        m_Attacking = StartCoroutine(AttackTargetRoutine(target));
    }

    private IEnumerator AttackTargetRoutine(Target target)
    {
        while (true)
        {
            yield return m_Movement.FolowUntilInRange(target, m_Entity.Stats.AttackRange);

            if (m_CanAttack && m_Entity.CanAttack)
            {
                IsAttacking = true;
                // OnAttackPerformed.Invoke();
                m_Entity.AttackTarget(target);
                StartCoroutine(WaitForNextAttack());
            }

            //Stops m_Attacking enemy that already died
            if(target.transform.GetComponent<EntityScript>().isDead)
            {
                IsAttacking = false;
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator WaitForNextAttack()
    {
        m_CanAttack = false;
        yield return new WaitForSeconds(m_Entity.GetAttackCooldown());
        m_CanAttack = true;
        yield break;
    }
}
