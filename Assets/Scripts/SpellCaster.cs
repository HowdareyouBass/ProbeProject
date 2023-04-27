using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Movement movement;
    private IEntity entity;
    private IController controller;
    private Coroutine spellCasting;
    private Spell.Types spellType;

    void Start()
    {
        entity = GetComponent<IEntity>();
        controller = GetComponent<IController>();
    }

    public void CastSpell(int spellSlot, RaycastHit target)
    {
        entity.SetCurrentSpell(spellSlot);
        spellType = entity.GetCurrentSpell().GetSpellType();

        controller.StopActions();

        if (spellType == Spell.Types.projectile || spellType == Spell.Types.directedAtEnemy && target.transform.CompareTag("Enemy"))
        {
            CastSpellOnEnemy(target);
        }
        else if (spellType == Spell.Types.directedAtGround)
        {
            CastSpellOnGround(target);
        }
        else if (spellType == Spell.Types.playerCast || spellType == Spell.Types.passiveSwitchable)
        {
            CastSpellSelf();
        }
    }

    private void CastSpellOnEnemy(RaycastHit target)
    {
        float targetRadius;
        if (target.transform.CompareTag("Player"))
        {
            targetRadius = Player.PLAYER_RADIUS;
        }
        else
        {
            targetRadius = target.collider.bounds.size.z;
        }
        spellCasting = StartCoroutine(CastSpellRoutine(target, targetRadius, false));
    }
    private void CastSpellOnGround(RaycastHit target)
    {
        spellCasting = StartCoroutine(CastSpellRoutine(target, 0, true));
    }
    private void CastSpellSelf()
    {
        //Don't need target because it's self cast
        entity.CastSpell(new RaycastHit());
    }
    private IEnumerator CastSpellRoutine(RaycastHit target, float targetRadius, bool isGround)
    {

        float castRange = entity.GetCurrentSpell().GetCastRange();
        while(EntityMath.VectorToNearestPoint(transform, target).magnitude > castRange && castRange != 0)
        {
            if (isGround)
            {
                movement.MoveToPoint(target.point);
            }
            else
            {   
                movement.MoveToEntity(target, targetRadius);
            }
            yield return null;
        }
        movement.Stop();
        movement.LookAtTarget(target);
        entity.CastSpell(target);
        yield break;
    }
}
