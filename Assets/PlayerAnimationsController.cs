using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    private NavMeshAgent m_Agent;
    private int m_SpeedHash;
    
    private void Start()
    {
        m_SpeedHash = Animator.StringToHash("Speed");
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        m_Animator.SetFloat(m_SpeedHash, m_Agent.velocity.magnitude / m_Agent.speed);
    }
}
