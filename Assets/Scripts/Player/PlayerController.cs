using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, IController
{
    [SerializeField] private Attack attack;
    [SerializeField] private Movement movement;
    [SerializeField] private SpellCaster spellCaster;

    [SerializeField] private GameObject movementEffect;

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
        attack?.AttackTarget(target);
        SpawnEffect(target.transform.position - new Vector3(0, targetHeight / 2, 0), Vector3.up);
    }
    private void MoveToTarget(RaycastHit target)
    {
        StopActions();
        movement.MoveToPoint(target.point);
        SpawnEffect(target.point, target.normal);
    }

    //TODO: Вынести в отдельный класс для спавна эффектов наверое?
    public void SpawnEffect(Vector3 position, Vector3 rotation)
    {
        Instantiate(movementEffect, position, Quaternion.LookRotation(rotation));
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

