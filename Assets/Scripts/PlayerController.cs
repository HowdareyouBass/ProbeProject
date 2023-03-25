using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public PlayerBehaviour player;
    public Camera playerCamera;
    public GameObject movementEffect;
    public NavMeshAgent agent;
    public bool paused;

    private Coroutine objectFollowing;
    private Coroutine objectAttacking;
    private Coroutine lookingAtTarget;
    private NavMeshObstacle objectFollowedNavMesh;
    private float playerRadius = 1f;
    private bool canAttack;

    private void Start()
    {
        canAttack = true;
        //agent.updateRotation = false;
        StartCoroutine(HoldingMouse());
    }
    private void Update()
    {
    }
    private IEnumerator HoldingMouse()
    {
        while(!paused)
        {
            if (Input.GetMouseButton(1))
            {
                OnClick();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
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
                LookAtTarget(hit.transform.position);
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
        //Disable NavMeshObjstacle component so that agent dont't try to avoid it
        if (objectFollowedNavMesh != null)
            objectFollowedNavMesh.enabled = true;
        objectFollowedNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        objectFollowedNavMesh.enabled = false;

        StopAttacking();
        Vector3 targetMesurments = target.collider.bounds.size;
        objectAttacking = StartCoroutine(AttackTargetRoutine(target, targetMesurments.z));
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
                yield return null;
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
        if (objectFollowedNavMesh != null)
            objectFollowedNavMesh.enabled = true;
        objectFollowedNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        objectFollowedNavMesh.enabled = false;

        StopFollowing();
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
        if (objectFollowedNavMesh != null)
            objectFollowedNavMesh.enabled = true;
        StopAttacking();
        StopFollowing();
        MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }
    
    private void LookAtTarget(Vector3 targetPosition)
    {
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
        lookingAtTarget = StartCoroutine(LookAtTargetRoutine(targetPosition));
    }
    private IEnumerator LookAtTargetRoutine(Vector3 targetPosition)
    {
        Vector3 lookRotation;
        float time;
        time = 0f;
        lookRotation = targetPosition - transform.position;
        lookRotation.y = 0;
        while (time < 1f)
        {
            transform.forward = Vector3.Slerp(transform.forward, lookRotation, time);
            time += Time.deltaTime * 1f;
            yield return null;
        }
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

    private void StopAttacking()
    {
        if (objectAttacking != null)
            StopCoroutine(objectAttacking);
    }

    private void StopFollowing()
    {
        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
    }
}

