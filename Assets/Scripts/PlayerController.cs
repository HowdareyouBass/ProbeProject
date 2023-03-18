using System.Collections;

using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject movementEffect;
    public NavMeshAgent agent;
    private bool following;
    private IEnumerator objectFollowing;
    private NavMeshObstacle objectFollowed;
    private float playerRadius = 1f;

    private Vector3 tarPos;
    private Vector3 v1;

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
                    FollowTarget(hit);
                }
                else
                {
                    MoveToTarget(hit);
                }
            }
        }
    }
    void FollowTarget(RaycastHit target)
    {
        //Disable NavMeshObjstacle component so that agent dont't try to avoid it
        if (objectFollowed != null)
            objectFollowed.enabled = true;
        objectFollowed = target.transform.GetComponent<NavMeshObstacle>();
        objectFollowed.enabled = false;

        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        Vector3 targetMesurments = target.collider.bounds.size;
        objectFollowing = FollowTargetRoutine(target, targetMesurments.z);
        Debug.Log(targetMesurments.x);
        Debug.Log(targetMesurments.z);
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
        if (objectFollowed != null)
            objectFollowed.enabled = true;
        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }
    void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    void MoveToEntity(Vector3 destination, float radiusOfObjectToFollow)
    {
        Vector3 toPlayerFromEnemyNormalized = Vector3.Normalize(transform.position - destination) / 2;
        Vector3 toEnemyNearestPoint = (destination - transform.position) + toPlayerFromEnemyNormalized * radiusOfObjectToFollow + toPlayerFromEnemyNormalized * playerRadius;
        v1 = Vector3.Normalize(transform.position - destination) / 2;
        tarPos = destination;
        agent.SetDestination(transform.position + toEnemyNearestPoint);
    }
    void SpawnEffect(Vector3 effectPosition, Vector3 effectRotation)
    {   
        GameObject effect = Instantiate(movementEffect, effectPosition, Quaternion.LookRotation(effectRotation));
        Destroy(effect, 2f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (v1 == null)
            return;
        Gizmos.DrawLine(tarPos, tarPos + v1);
    }
}

