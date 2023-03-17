using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public Camera playerCamera;
    public GameObject movementEffect;

    public NavMeshAgent agent;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 destinaton;
            Vector3 effectPosition;
            Quaternion effectRotation;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Vector3 enemyMesurments = hit.collider.bounds.size;
                    Vector3 vectorToEnemyNearestPoint = (hit.transform.position - transform.position) + Vector3.Normalize(transform.position - hit.transform.position) * enemyMesurments.x;
                    destinaton = transform.position + vectorToEnemyNearestPoint;
                    effectPosition = hit.transform.position - new Vector3(0, enemyMesurments.y / 2, 0);
                    effectRotation = Quaternion.LookRotation(Vector3.up);
                }

                else
                {
                    destinaton = hit.point;
                    effectPosition = hit.point;
                    effectRotation = Quaternion.LookRotation(hit.normal);
                }

                agent.SetDestination(destinaton);
                GameObject effect = Instantiate(movementEffect, effectPosition, effectRotation);
                Destroy(effect, 2f);
            }
        }
    }
}
