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
    IEnumerator FollowTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(true)
        {
            MoveToEntity(target.transform.position, targetRadius);
            yield return null;
        }
    }
    void MoveToEntity(Vector3 destination, float radiusOfObjectToFollow)
    {
        Vector3 vectorToEnemyNearestPoint = (destination - transform.position) + Vector3.Normalize(transform.position - destination) * radiusOfObjectToFollow;
        agent.SetDestination(transform.position + vectorToEnemyNearestPoint);
    }
    void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    void FollowTarget(RaycastHit target)
    {
        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        Vector3 targetMesurments = target.collider.bounds.size;
        objectFollowing = FollowTargetRoutine(target, targetMesurments.x);
        StartCoroutine(objectFollowing);
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    void MoveToTarget(RaycastHit target)
    {
        if (objectFollowing != null)
            StopCoroutine(objectFollowing);
        MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }
    void SpawnEffect(Vector3 effectPosition, Vector3 effectRotation)
    {   
        GameObject effect = Instantiate(movementEffect, effectPosition, Quaternion.LookRotation(effectRotation));
        Destroy(effect, 2f);
    }
}

