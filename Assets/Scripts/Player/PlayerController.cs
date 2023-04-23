using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerBehaviour player;
    public GameObject movementEffect;
    public NavMeshAgent agent;
    public float rotationSpeed;

    private Coroutine playerIsFollowing;
    private Coroutine playerIsAttacking;
    private Coroutine playerSpellCasting;
    private Coroutine lookingAtTarget;
    private NavMeshObstacle targetNavMesh;
    private float playerRadius = 1f;
    private bool canAttack = true;

    void Start()
    {
        StopAction();
    }

    public void OnClick(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Enemy"))
        {
            AttackTarget(hit);
        }
        else
        {
            MoveToTarget(hit);
        }
    }

    public void CastSpell(int spellSlot, RaycastHit target)
    {
        player.SetCurrentSpell(spellSlot);
        Spell.Types spellType = player.GetCurrentSpell().GetSpellType();
        if (target.transform.CompareTag("Enemy") && spellType == Spell.Types.directedAtEnemy)
        {
            CastSpellOnEnemy(target);
        }
        else if (spellType == Spell.Types.directedAtGround)
        {
            CastSpellOnGround(target);
        }
    }

    private void CastSpellOnEnemy(RaycastHit target)
    {
        if (targetNavMesh != null)
            targetNavMesh.enabled = true;
        targetNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        targetNavMesh.enabled = false;
        Vector3 targetMesurments = target.collider.bounds.size;
        StopAction();
        playerSpellCasting = StartCoroutine(CastSpellOnTargetRoutine(target, targetMesurments.z));
    }
    private IEnumerator CastSpellOnTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(FindNearestPointToEntity(target.transform.position, targetRadius).magnitude > player.GetCurrentSpell().GetCastRange() && player.GetCurrentSpell().GetCastRange() != 0)
        {
            MoveToEntity(target.transform.position, targetRadius);
            yield return null;
        }
        StopMoving();
        LookAtTarget(target);
        player.CastSpell(target);
        yield break;
    }

    public void LookAtTarget(RaycastHit target)
    {
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
        lookingAtTarget = StartCoroutine(LookAtTargetRoutine(target));
    }
    private IEnumerator LookAtTargetRoutine(RaycastHit target)
    {
        Vector3 lookRotation = target.transform.position - transform.position;
        float delta = 0.001f;
        //Difference between desired looking rotation and current looking rotation
        Vector3 difference = lookRotation - transform.forward;
        while(Mathf.Abs(difference.x) > delta || Mathf.Abs(difference.z) > delta)
        {
            if (target.transform == null)
                yield break;
            lookRotation = target.transform.position - transform.position;
            //Set y to 0 so that player GameObject don't rotate up and down
            lookRotation.y = 0;
            transform.forward = Vector3.MoveTowards(transform.forward, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void CastSpellOnGround(RaycastHit target)
    {
        StopAction();
        playerSpellCasting = StartCoroutine(CastSpellOnGroundRoutine(target));
    }
    private IEnumerator CastSpellOnGroundRoutine(RaycastHit target)
    {
        while(FindNearestPointToEntity(target.point, 0).magnitude > player.GetCurrentSpell().GetCastRange() && player.GetCurrentSpell().GetCastRange() != 0)
        {
            MoveToEntity(target.point, 0);
            yield return null;
        }
        StopMoving();
        LookAtPoint(target.point);
        player.CastSpell(target);
        yield break;
    }

    private void LookAtPoint(Vector3 point)
    {
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
        lookingAtTarget = StartCoroutine(LookAtPointRoutine(point));
    }

    private IEnumerator LookAtPointRoutine(Vector3 point)
    {
        Vector3 lookRotation = point - transform.position;
        float delta = 0.001f;
        //Difference between desired looking rotation and current looking rotation
        Vector3 difference = lookRotation - transform.forward;
        while(Mathf.Abs(difference.x) > delta || Mathf.Abs(difference.z) > delta)
        {
            lookRotation = point - transform.position;
            //Set y to 0 so that player GameObject don't rotate up and down
            lookRotation.y = 0;
            transform.forward = Vector3.MoveTowards(transform.forward, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }

    private void AttackTarget(RaycastHit target)
    {
        //Enable previous enemy attacked NavMeshObstacle component
        if (targetNavMesh != null)
            targetNavMesh.enabled = true;
        //Disable NavMeshObstacle component so that agent dont't try to avoid
        targetNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        targetNavMesh.enabled = false;

        StopAction();
        //LockAtTarget(target);
        LookAtTarget(target);
        Vector3 targetMesurments = target.collider.bounds.size;
        playerIsAttacking = StartCoroutine(AttackTargetRoutine(target, targetMesurments.z));
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    private IEnumerator AttackTargetRoutine(RaycastHit target, float targetRadius)
    {
        while (true)
        {
            while(FindNearestPointToEntity(target.transform.position, targetRadius).magnitude > player.GetAttackRange())
            {
                MoveToEntity(target.transform.position, targetRadius);
                yield return null;
            }
            StopMoving();
            while(FindNearestPointToEntity(target.transform.position, targetRadius).magnitude <= player.GetAttackRange())
            {
                if (canAttack)
                {
                    player.AttackTarget(target);
                    StartCoroutine(WaitForNextAttack());
                }

                //Stops attacking enemy that already died
                if(target.transform.GetComponent<EnemyBehavior>().isDead)
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
        yield return new WaitForSeconds(player.GetAttackCooldown());
        canAttack = true;
        yield break;
    }

    private void FollowTarget(RaycastHit target)
    {
        //Disable NavMeshObjstacle component so that agent dont't try to avoid it
        if (targetNavMesh != null)
            targetNavMesh.enabled = true;
        targetNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        targetNavMesh.enabled = false;

        StopAction();
        Vector3 targetMesurments = target.collider.bounds.size;
        playerIsFollowing = StartCoroutine(FollowTargetRoutine(target, targetMesurments.z));
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    private IEnumerator FollowTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(true)
        {
            MoveToEntity(target.transform.position, targetRadius);
            yield return null;
        }
    }

    private void MoveToTarget(RaycastHit target)
    {
        if (targetNavMesh != null)
            targetNavMesh.enabled = true;
        StopAction();
        MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }
    private void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    private void MoveToEntity(Vector3 destination, float radiusOfEntity)
    {
        agent.SetDestination(transform.position + FindNearestPointToEntity(destination, radiusOfEntity));
    }
    private void SpawnEffect(Vector3 effectPosition, Vector3 effectRotation)
    {   
        GameObject effect = Instantiate(movementEffect, effectPosition, Quaternion.LookRotation(effectRotation));
        Destroy(effect, 2f);
    }
    private Vector3 FindNearestPointToEntity(Vector3 entityPosition, float entityRadius)
    {
        if (entityRadius == 0)
        {
            return entityPosition - transform.position;
        }
        Vector3 toPlayerFromEntityNormalized = Vector3.Normalize(transform.position - entityPosition) / 2;
        return (entityPosition - transform.position) + toPlayerFromEntityNormalized * (entityRadius + playerRadius);
    }

    private void StopMoving()
    {
        agent.SetDestination(transform.position);
    }
    public void StopAction()
    {
        agent.SetDestination(transform.position);
        if (playerIsFollowing != null)
            StopCoroutine(playerIsFollowing);
        if (playerIsAttacking != null)
            StopCoroutine(playerIsAttacking);
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
        if (playerSpellCasting != null)
            StopCoroutine(playerSpellCasting);
    }
}

