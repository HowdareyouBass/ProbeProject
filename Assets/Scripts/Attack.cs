using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Movement m_Movement;
    private Entity m_Entity;
    private EntityController m_Controller;
    private GameEvent<Transform> m_OnAttack;
    private Coroutine m_Attacking;
    private bool m_CanAttack = true;

    private void Start()
    {
        m_Movement = GetComponent<Movement>();
        m_Controller = GetComponent<EntityController>();
        m_Entity = GetComponent<EntityScript>().GetEntity();
        m_OnAttack = m_Entity.events.GetEvent<Transform>(EntityEventName.OnAttack, true);
        m_Entity.events.GetEvent(EntityEventName.OnAttackDisabled).Subscribe(Stop);
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
            yield return m_Movement.FolowUntilInRange(target, m_Entity.stats.attackRange);
            if (m_CanAttack && m_Entity.canAttack)
            {
                m_OnAttack?.Trigger(target.transform);
                m_Entity.DamageTarget(target.transform);
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
