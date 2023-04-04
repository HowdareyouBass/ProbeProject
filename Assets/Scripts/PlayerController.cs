using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerBehaviour player;
    public Camera playerCamera;
    public GameObject movementEffect;
    public NavMeshAgent agent;
    public float rotationSpeed;

    private Coroutine objectFollowing;
    private Coroutine objectAttacking;
    private Coroutine lookingAtTarget;
    private NavMeshObstacle targetNavMesh;
    private float playerRadius = 1f;
    private float timer = 0;
    private bool canAttack = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.CastSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopAction();
        }
        if (timer >= 0.1f && Input.GetMouseButton(1))
        {
            OnClick();
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void OnClick()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        //If clicked on something
        if (Physics.Raycast(ray, out hit))
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
    }

    private void AttackTarget(RaycastHit target)
    {
        //Disable NavMeshObjstacle component so that agent dont't try to avoid target
        if (targetNavMesh != null)
            targetNavMesh.enabled = true;
        targetNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        targetNavMesh.enabled = false;

        StopAction();
        LookAtTarget(target);
        Vector3 targetMesurments = target.collider.bounds.size;
        objectAttacking = StartCoroutine(AttackTargetRoutine(target, targetMesurments.z));
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    private void LookAtTarget(RaycastHit target)
    {
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
        lookingAtTarget = StartCoroutine(LookAtTargetRoutine(target));
    }
    private IEnumerator LookAtTargetRoutine(RaycastHit target)
    {
        Vector3 lookRotation = target.transform.position - transform.position;
        while(transform.forward != lookRotation)
        {
            if (target.transform == null)
                yield break;
            lookRotation = target.transform.position - transform.position;
            lookRotation.y = 0;
            transform.forward = Vector3.MoveTowards(transform.forward, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
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

                //Stops attacking object that already died
                if(target.transform.GetComponent<EnemyBehavior>().isDead)
                {
                    Debug.Log("ded");
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
        objectFollowing = StartCoroutine(FollowTargetRoutine(target, targetMesurments.z));
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
        Vector3 toPlayerFromEntityNormalized = Vector3.Normalize(transform.position - entityPosition) / 2;
        return (entityPosition - transform.position) + toPlayerFromEntityNormalized * entityRadius + toPlayerFromEntityNormalized * playerRadius;
    }

    private void StopMoving()
    {
        agent.SetDestination(transform.position);
    }
    private void StopAction()
    {
        agent.SetDestination(transform.position);
        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        if (objectAttacking != null)
            StopCoroutine(objectAttacking);
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
    }
}

