using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;

    private NavMeshAgent agent;
    private Coroutine looking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToEntity(RaycastHit target, float radiusOfEntity)
    {
        agent.SetDestination(transform.position + EntityMath.VectorToNearestPoint(transform, target));
    }
    public void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public void LookAtTarget(RaycastHit target)
    {
        if (looking != null)
            StopCoroutine(looking);
        looking = StartCoroutine(LookAtTargetRoutine(target));
    }
    private IEnumerator LookAtTargetRoutine(RaycastHit target)
    {
        Vector3 lookRotation = target.transform.position - transform.position;
        
        float delta = 0.5f;
        float angle = Vector3.Angle(transform.forward, lookRotation);
        while(Mathf.Abs(angle) > delta)
        {
            if (target.transform == null)
                yield break;
            lookRotation = target.transform.position - transform.position;
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
