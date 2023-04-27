using System.Collections;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attack))]
public class PlayerController : MonoBehaviour, IController
{
    public static PlayerController instance = null;

    [SerializeField] private Health health;
    [SerializeField] private Attack attack;
    [SerializeField] private Movement movement;
    [SerializeField] private SpellCaster spellCaster;

    [SerializeField] private GameObject movementEffect;
    [SerializeField] private NavMeshAgent agent;

    private Player player;
    private Coroutine playerIsFollowing;
    private Coroutine playerIsAttacking;
    private Coroutine playerSpellCasting;
    private Coroutine lookingAtTarget;
    private NavMeshObstacle targetNavMesh;
    private float playerRadius = 1f;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : Can only be one PlayerController component!");
        }
    }

    void Start()
    {
        player = Player.instance;
        StopActions();
    }

    public void StopActions()
    {
        attack.Stop();
        movement.Stop();
    }

    public void OnClick(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Enemy"))
        {
            AttackTarget(hit);
        }
        else
        {
            MoveToTarget(hit);
        }
    }

    public void Damage(float amount)
    {
        health.Damage(amount);
    }

    public void CastSpell(int spellSlot, RaycastHit target)
    {
        spellCaster.CastSpell(spellSlot, target);
    }
    private IEnumerator CastSpellOnTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(FindNearestPointToEntity(target.transform.position, targetRadius).magnitude > player.GetCurrentSpell().GetCastRange() && player.GetCurrentSpell().GetCastRange() != 0)
        {
            MoveToEntity(target.transform.position, targetRadius);
            yield return null;
        }
        StopMoving();
        //LookAtTarget(target);
        player.CastSpell(target);
        yield break;
    }

    private void CastSpellOnGround(RaycastHit target)
    {
        StopAction();
        playerSpellCasting = StartCoroutine(CastSpellOnGroundRoutine(target));
    }
    private IEnumerator CastSpellOnGroundRoutine(RaycastHit target)
    {
        while(FindNearestPointToEntity(target.point, 0).magnitude > player.GetCurrentSpell().GetCastRange() && player.GetCurrentSpell().GetCastRange() != 0)
        {
            MoveToEntity(target.point, 0);
            yield return null;
        }
        StopMoving();
        //LookAtPoint(target.point);
        player.CastSpell(target);
        yield break;
    }

    private void AttackTarget(RaycastHit target)
    {
        attack.AttackTarget(target);
        SpawnEffect(target.transform.position - Vector3.up * (target.collider.bounds.size.y / 2), Vector3.up);
    }

    private void FollowTarget(RaycastHit target)
    {
        //Disable NavMeshObjstacle component so that agent dont't try to avoid it
        if (targetNavMesh != null)
            targetNavMesh.enabled = true;
        targetNavMesh = target.transform.GetComponent<NavMeshObstacle>();
        targetNavMesh.enabled = false;

        StopAction();
        Vector3 targetMesurments = target.collider.bounds.size;
        playerIsFollowing = StartCoroutine(FollowTargetRoutine(target, targetMesurments.z));
        SpawnEffect(target.transform.position - new Vector3(0, targetMesurments.y / 2, 0), Vector3.up);
    }
    private IEnumerator FollowTargetRoutine(RaycastHit target, float targetRadius)
    {
        while(true)
        {
            MoveToEntity(target.transform.position, targetRadius);
            yield return null;
        }
    }

    private void MoveToTarget(RaycastHit target)
    {
        StopActions();
        movement.MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }
    private void MoveToPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    private void MoveToEntity(Vector3 destination, float radiusOfEntity)
    {
        agent.SetDestination(transform.position + FindNearestPointToEntity(destination, radiusOfEntity));
    }
    private void SpawnEffect(Vector3 effectPosition, Vector3 effectRotation)
    {   
        GameObject effect = Instantiate(movementEffect, effectPosition, Quaternion.LookRotation(effectRotation));
        Destroy(effect, 2f);
    }
    private Vector3 FindNearestPointToEntity(Vector3 entityPosition, float entityRadius)
    {
        if (entityRadius == 0)
        {
            return entityPosition - transform.position;
        }
        Vector3 toPlayerFromEntityNormalized = Vector3.Normalize(transform.position - entityPosition) / 2;
        return (entityPosition - transform.position) + toPlayerFromEntityNormalized * (entityRadius + playerRadius);
    }

    private void StopMoving()
    {
        agent.SetDestination(transform.position);
    }
    public void StopAction()
    {
        agent.SetDestination(transform.position);
        if (playerIsFollowing != null)
            StopCoroutine(playerIsFollowing);
        if (playerIsAttacking != null)
            StopCoroutine(playerIsAttacking);
        if (lookingAtTarget != null)
            StopCoroutine(lookingAtTarget);
        if (playerSpellCasting != null)
            StopCoroutine(playerSpellCasting);
    }
}

