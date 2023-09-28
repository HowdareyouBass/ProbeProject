using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Movement m_Movement;
    private Entity m_Entity;
    private EntityController m_Controller;
    private Coroutine m_Attacking;
    private bool m_CanAttack = true;

    private void Start()
    {
        m_Movement = GetComponent<Movement>();
        m_Controller = GetComponent<EntityController>();
        m_Entity = GetComponent<EntityScript>().GetEntity();
    }

    public void Stop()
    {
        if (m_Attacking != null)
            StopCoroutine(m_Attacking);
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
                m_Entity.AttackTarget(target);
                StartCoroutine(WaitForNextAttack());
            }

            //Stops m_Attacking enemy that already died
            if(target.transform.GetComponent<EntityScript>().isDead)
            {
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
