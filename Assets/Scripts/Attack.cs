using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Attack : MonoBehaviour
{
    [SerializeField] private IController controller;
    [SerializeField] private Entity entity;
    [SerializeField] private Movement movement;

    private GameEvent OnAttack;
    private Coroutine attacking;
    private bool canAttack = true;

    void Start()
    {
        controller = GetComponent<IController>();
        entity = GetComponent<EntityScript>().GetEntity();
        OnAttack = entity.GetOnAttackEvent();
    }

    public void Stop()
    {
        if (attacking != null)
            StopCoroutine(attacking);
    }
    public void AttackTarget(RaycastHit target)
    {
        controller.StopActions();

        float targetRadius;
        if (target.transform.CompareTag("Player"))
        {
            targetRadius = Player.PLAYER_RADIUS;
        }
        else
        {
            targetRadius = target.collider.bounds.size.z;
        }
        attacking = StartCoroutine(AttackTargetRoutine(target, targetRadius));
    }

    private IEnumerator AttackTargetRoutine(RaycastHit target, float targetRadius)
    {
        while (true)
        {
            while(EntityMath.VectorToNearestPoint(transform, target).magnitude > entity.GetAttackRange())
            {
                movement.MoveToEntity(target, targetRadius);
                yield return null;
            }
            movement.Stop();
            while(EntityMath.VectorToNearestPoint(transform, target).magnitude <= entity.GetAttackRange())
            {
                movement.LookAtTarget(target, true);
                if (canAttack)
                {
                    OnAttack?.Trigger();
                    entity.DamageTarget(target);
                    StartCoroutine(WaitForNextAttack());
                }

                //Stops attacking enemy that already died
                if(target.transform.GetComponent<EnemyScript>().isDead)
                {
                    yield break;
                }

                yield return null;;
            }
        }
    }

    private IEnumerator WaitForNextAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(entity.GetAttackCooldown());
        canAttack = true;
        yield break;
    }
}
