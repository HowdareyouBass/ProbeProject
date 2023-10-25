using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private int m_RotationSpeed;
    
    private NavMeshAgent m_Agent;
    private EntityController m_Controller;
    private Entity m_Entity;
    private Coroutine m_Looking;
    private Coroutine m_Patroling;

    private void Awake()
    {
        m_Entity = GetComponent<EntityScript>().GetEntity();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Controller = GetComponent<EntityController>();
    }
    private void OnEnable()
    {
        m_Entity.Events.GetEvent(EntityEventName.StopMovement).Subscribe(Stop);
    }
    private void OnDisable()
    {
        m_Entity.Events.GetEvent(EntityEventName.StopMovement).Unsubscribe(Stop);
        StopAllCoroutines();
    }

    public void Move(Vector3 destination)
    {
        if (m_Entity.CanMove && m_Agent != null && m_Agent.enabled)
            m_Agent.SetDestination(destination);
    }
    public IEnumerator FollowUntilInRange(Target target, int range)
    {
        //Move while range is less then vector to target magnitude
        while(target.GetVector(transform).magnitude > range && range != 0)
        {
            Move(target.GetPoint());
            yield return null;
        }
        Stop();
        Look(target);
    }

    public void PatrolPoints(LinkedList<Vector3> points)
    {
        m_Patroling = StartCoroutine(PatrolingPointsRoutine(points));
    }
    private IEnumerator PatrolingPointsRoutine(LinkedList<Vector3> points)
    {
        LinkedListNode<Vector3> point = points.First;
        // while (point != null)
        // {
        //     GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        //     go.transform.position = point.Value;
        //     point = point.Next;
        // }
        while (point != null)
        {
            Move(point.Value);
            while (m_Agent.remainingDistance > 0.1f)
            {
                yield return null;
            }
            if (point == points.Last)
            {
                point = points.First;
                continue;
            }
            point = point.Next;
        }
    }
    private void Look(Target target)
    {
        if (m_Entity.CanLook)
        {
	        if (m_Looking != null)
	            StopCoroutine(m_Looking);
	        m_Looking = StartCoroutine(LookRoutine(target));
        }
    }
    private IEnumerator LookRoutine(Target target)
    {
        Vector3 lookRotation = Vector3.Normalize(target.GetVector(transform));
        float delta = 0.5f;
        float angle = Vector3.Angle(transform.forward, lookRotation);

        while(Mathf.Abs(angle) > delta)
        {
            if (target.transform == null)
                yield break;
            lookRotation = Vector3.Normalize(target.GetVector(transform));
            angle = Vector3.Angle(lookRotation, transform.forward);
            //Set y to 0 so that player GameObject don't rotate up and down
            lookRotation.y = 0;
            transform.forward = Vector3.MoveTowards(transform.forward, lookRotation, m_RotationSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    
    public void Stop()
    {
        if (m_Agent != null && m_Agent.enabled)
            m_Agent.SetDestination(transform.position);
        if (m_Looking != null)
            StopCoroutine(m_Looking);
        if (m_Patroling != null)
            StopCoroutine(m_Patroling);
    }
}
