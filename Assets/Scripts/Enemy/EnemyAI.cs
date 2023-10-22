using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle = 90f;
    public float viewDistance = 15f;
    public float detectionDistance = 3f;

    public Transform enemyEye;
    public Transform[] goals;
    public float distanceToChangeGoal;

    private int currentGoal = 0;

    public Transform player;

    private Coroutine m_CheckingForPlayer;

    private EnemyController m_Controller;

    private void Start()
    {
        m_Controller = GetComponent<EnemyController>();

        m_CheckingForPlayer = StartCoroutine(CheckForPlayerRoutine());
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
