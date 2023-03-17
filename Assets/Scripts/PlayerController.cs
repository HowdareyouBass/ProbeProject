using System.Collections;

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
            Vector3 effectPosition;
            Quaternion effectRotation;
           
            //If clicked on something
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Vector3 enemyMesurments = hit.collider.bounds.size;
                    StartCoroutine(FollowTarget(hit, enemyMesurments.x));
                    effectPosition = hit.transform.position - new Vector3(0, enemyMesurments.y / 2, 0);
                    effectRotation = Quaternion.LookRotation(Vector3.up);
                }
                else
                {
                    Move(hit.point);
                    effectPosition = hit.point;
                    effectRotation = Quaternion.LookRotation(hit.normal);
                }

                GameObject effect = Instantiate(movementEffect, effectPosition, effectRotation);
                Destroy(effect, 2f);
            }
        }
    }

    IEnumerator FollowTarget(RaycastHit target, float targetRadius)
    {
        while(true)
        {
            Move(target.transform.position, targetRadius);
            yield return null;
        }
    }
    void Move(Vector3 destination, float radiusOfObjectToFollow)
    {
        Vector3 vectorToEnemyNearestPoint = (destination - transform.position) + Vector3.Normalize(transform.position - destination) * radiusOfObjectToFollow;
        agent.SetDestination(transform.position + vectorToEnemyNearestPoint);
    }
    void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}

