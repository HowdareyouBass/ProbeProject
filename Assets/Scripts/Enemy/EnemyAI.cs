using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle = 90f;
    public float viewDistance = 15f;
    public float detectionDistance = 3f;

    [SerializeField] private Transform m_PatrolObject;

    public Transform enemyEye;
    public Transform player;
    private Coroutine m_CheckingForPlayer;
    private EnemyController m_Controller;

    private LinkedList<Vector3> m_PatrolPoints;

    private void Start()
    {

        m_Controller = GetComponent<EnemyController>();


        // If No patrol object don't do the convesion
        m_CheckingForPlayer = StartCoroutine(CheckForPlayerRoutine());

        Transform[] patrolObjects = m_PatrolObject.GetComponentsInChildren<Transform>();
        if (patrolObjects.Length == 1)
            return;
        m_PatrolPoints = new LinkedList<Vector3>();
        for (int i = 1; i < patrolObjects.Length; i++)
        {
            m_PatrolPoints.AddFirst(new LinkedListNode<Vector3>(patrolObjects[i].position));
        }
        m_Controller.PatrolPoints(m_PatrolPoints);
    }

    private IEnumerator CheckForPlayerRoutine()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= detectionDistance || IsInView())
            {
                if (m_Controller.AllSpellsOnCooldown())
                {
                    Debug.Log("Should attack");
                    m_Controller.Attack(new Target(player));
                }
                else
                {
                    Debug.Log("Should cast");
                    m_Controller.CastSpellNotOnCooldown(new Target(player));
                }
            }
            // else
            // {
            //     m_Controller.PatrolPoints(m_PatrolPoints);
            // }

            // if (agent.remainingDistance < distanceToChangeGoal)
            // {
            //     currentGoal++;
            //     if (currentGoal == goals.Length) currentGoal = 0;
            //     agent.destination = goals[currentGoal].position;
            // }
            yield return null;
        }
    }

    private bool IsInView()
    {
        float realAngle = Vector3.Angle(enemyEye.forward, player.position - enemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(enemyEye.transform.position, player.position - enemyEye.position, out hit, viewDistance))
        {
            if (realAngle < viewAngle / 2f && Vector3.Distance(enemyEye.position, player.position) <= viewDistance && hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 left = enemyEye.position + Quaternion.Euler(new Vector3(0, viewAngle / 2f, 0)) * (enemyEye.forward * viewDistance);
        Vector3 right = enemyEye.position + Quaternion.Euler(-new Vector3(0, viewAngle / 2f, 0)) * (enemyEye.forward * viewDistance);
        Gizmos.DrawLine(enemyEye.position, left);
        Gizmos.DrawLine(enemyEye.position, right);
    }
}
