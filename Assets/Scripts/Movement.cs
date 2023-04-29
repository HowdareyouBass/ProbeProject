using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;

    private NavMeshAgent agent;
    private IController controller;
    private Coroutine looking;
    private Coroutine following;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<IController>();
    }

    public void FollowTarget(RaycastHit target)
    {
        controller.StopActions();
        float targetRadius = target.collider.bounds.size.z;
        following = StartCoroutine(FollowTargetRoutine(target, targetRadius));
    }
    public void MoveToEntity(RaycastHit target, float radiusOfEntity)
    {
        agent.SetDestination(transform.position + EntityMath.VectorToNearestPoint(transform, target));
    }
    public void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public void LookAtTarget(RaycastHit target, bool isEntity)
    {
        if (looking != null)
            StopCoroutine(looking);
        looking = StartCoroutine(LookAtTargetRoutine(target, isEntity));
    }

    private IEnumerator FollowTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(true)
        {
            MoveToEntity(target, targetRadius);
            yield return null;
        }
    }
    private IEnumerator LookAtTargetRoutine(RaycastHit target, bool isEntity)
    {
        //if target is entity then we look at it's position if it's ground or prop we look at point of raycasthit
        Vector3 lookRotation = isEntity ? target.transform.position - transform.position : target.point - transform.position;
        
        float delta = 0.5f;
        float angle = Vector3.Angle(transform.forward, lookRotation);
        while(Mathf.Abs(angle) > delta)
        {
            if (target.transform == null)
                yield break;
            lookRotation = isEntity ? target.transform.position - transform.position : target.point - transform.position;
            angle = Vector3.Angle(lookRotation, transform.forward);
            //Set y to 0 so that player GameObject don't rotate up and down
            lookRotation.y = 0;
            transform.forward = Vector3.MoveTowards(transform.forward, lookRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    public void Stop()
    {
        agent.SetDestination(transform.position);
        if (looking != null)
            StopCoroutine(looking);
    }
}
