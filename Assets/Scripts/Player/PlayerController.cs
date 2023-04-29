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
        StopActions();
    }

    public void StopActions()
    {
        attack.Stop();
        movement.Stop();
        spellCaster.Stop();
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

    public void AttackTarget(RaycastHit target)
    {
        float targetHeight = target.collider.bounds.size.y;
        attack.AttackTarget(target);
        SpawnEffect(target.transform.position - new Vector3(0, targetHeight / 2, 0), Vector3.up);
    }
    private void MoveToTarget(RaycastHit target)
    {
        StopActions();
        movement.MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }

    public void SpawnEffect(Vector3 position, Vector3 rotation)
    {
        Instantiate(movementEffect, position, Quaternion.LookRotation(rotation));
    }
    //Test purposes
    public void Damage(float amount)
    {
        health.TakeDamage(amount);
    }

    public void CastSpell(int spellSlot, RaycastHit target)
    {
        spellCaster.CastSpell(spellSlot, target);
    }

    public void FollowTarget(RaycastHit target)
    {
        movement.FollowTarget(target);
    }
}

