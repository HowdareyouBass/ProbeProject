using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private int m_RotationSpeed;

    private NavMeshAgent m_Agent;
    private EntityController m_Controller;
    private Entity m_Entity;
    private Coroutine m_Looking;

    private Vector3 ls;

    private void Start()
    {
        m_Entity = GetComponent<EntityScript>().GetEntity();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Controller = GetComponent<EntityController>();
        m_Entity.GetEvent(EntityEventName.OnMovementDisabled).Subscribe(Stop);
    }

    public void Move(Vector3 destination)
    {
        if (m_Entity.canMove)
            m_Agent.SetDestination(destination);
    }
    public IEnumerator FolowUntilInRange(Target target, int range)
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
    private void Look(Target target)
    {
        if (m_Entity.canLook)
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
            ls = lookRotation;
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
        m_Agent.SetDestination(transform.position);
        if (m_Looking != null)
            StopCoroutine(m_Looking);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + ls);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
