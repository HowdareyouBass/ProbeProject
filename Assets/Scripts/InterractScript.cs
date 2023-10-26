using System.Collections;
using UnityEngine;

public class InterractScript : MonoBehaviour
{
    [SerializeField] private int m_InterractionRange = 3;

    private Coroutine m_Interracting;
    private EntityController m_Controller;
    private Movement m_Movement;

    private void Start()
    {
        m_Movement = GetComponent<Movement>();
        m_Controller = GetComponent<EntityController>();
    }

    public void Stop()
    {
        if (m_Interracting != null)
            StopCoroutine(m_Interracting);
    }

    public void Interract(Target target)
    {
        m_Controller.StopActions();
        m_Interracting = StartCoroutine(InterractRoutine(target));
    }

    private IEnumerator InterractRoutine(Target target)
    {
        while (true)
        {
            yield return m_Movement.FollowUntilInRange(target, m_InterractionRange);
            target.transform.GetComponent<InterractablePropScript>().Interract();
            yield break;
        }    
    }
}