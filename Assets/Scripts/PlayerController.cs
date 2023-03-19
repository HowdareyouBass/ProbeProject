using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject movementEffect;
    public NavMeshAgent agent;
    public Race playerRace;

    private IEnumerator objectFollowing;
    private IEnumerator objectAttacking;
    private NavMeshObstacle objectFollowedNavMesh;
    private float playerRadius = 1f;
    private PlayerStats stats;

    void Start()
    {
        stats = new PlayerStats();
        stats.setRace(playerRace);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
    }

    void AttackTarget(RaycastHit target)
    {
        //Disable NavMeshObjstacle component so that agent dont't try to avoid it
        if (objectFollowedNavMesh != null)
            objectFollowedNavMesh.enabled = true;
        objectFollowedNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        objectFollowedNavMesh.enabled = false;

        if(objectAttacking != null)
            StopCoroutine(objectAttacking);
        Vector3 targetMesurments = target.collider.bounds.size;
        objectAttacking = AttackTargetRoutine(target, targetMesurments.z);
        StartCoroutine(objectAttacking);
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    IEnumerator AttackTargetRoutine(RaycastHit target, float targetRadius)
    {
        while (true)
        {
            while(FindNearestPointToEntity(target.transform.position, targetRadius).magnitude > stats.GetAttackRange())
            {
                MoveToEntity(target.transform.position, targetRadius);
                yield return null;
            }
            StopMoving();
            while(FindNearestPointToEntity(target.transform.position, targetRadius).magnitude <= stats.GetAttackRange())
            {
                Attack(target);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    void FollowTarget(RaycastHit target)
    {
        //Disable NavMeshObjstacle component so that agent dont't try to avoid it
        if (objectFollowedNavMesh != null)
            objectFollowedNavMesh.enabled = true;
        objectFollowedNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        objectFollowedNavMesh.enabled = false;

        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        Vector3 targetMesurments = target.collider.bounds.size;
        objectFollowing = FollowTargetRoutine(target, targetMesurments.z);
        StartCoroutine(objectFollowing);
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    IEnumerator FollowTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(true)
        {
            MoveToEntity(target.transform.position, targetRadius);
            yield return null;
        }
    }

    void MoveToTarget(RaycastHit target)
    {
        if (objectFollowedNavMesh != null)
            objectFollowedNavMesh.enabled = true;
        if (objectAttacking != null)
            StopCoroutine(objectAttacking);
        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }
    void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    void MoveToEntity(Vector3 destination, float radiusOfEntity)
    {
        agent.SetDestination(transform.position + FindNearestPointToEntity(destination, radiusOfEntity));
    }
    void SpawnEffect(Vector3 effectPosition, Vector3 effectRotation)
    {   
        GameObject effect = Instantiate(movementEffect, effectPosition, Quaternion.LookRotation(effectRotation));
        Destroy(effect, 2f);
    }
    Vector3 FindNearestPointToEntity(Vector3 entityPosition, float entityRadius)
    {
        Vector3 toPlayerFromEntityNormalized = Vector3.Normalize(transform.position - entityPosition) / 2;
        return (entityPosition - transform.position) + toPlayerFromEntityNormalized * entityRadius + toPlayerFromEntityNormalized * playerRadius;
    }

    void Attack(RaycastHit target)
    {
        target.transform.GetComponent<EnemyBehavior>().Damage(stats.GetAttackDamage());
    }

    void StopMoving()
    {
        agent.SetDestination(transform.position);
    }
}

