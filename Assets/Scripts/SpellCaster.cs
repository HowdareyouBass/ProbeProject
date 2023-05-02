using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class SpellCaster : MonoBehaviour
{
    [SerializeField] private Movement movement;
    private Entity entity;
    private IController controller;
    private Coroutine spellCasting;
    private Spell1.Types spellType;
    private Spell1 currentSpell;

    void Start()
    {
        entity = GetComponent<EntityScript>().GetEntity();
        controller = GetComponent<IController>();
    }

    public void CastSpell(int spellSlot, RaycastHit target)
    {
        controller.StopActions();
        currentSpell = entity.GetSpellFromSlot(spellSlot);
        spellType = currentSpell.GetSpellType();

        if ((spellType == Spell1.Types.projectile || spellType == Spell1.Types.directedAtEnemy) && target.transform.CompareTag("Enemy"))
        {
            CastSpellOnEnemy(target);
        }
        if (spellType == Spell1.Types.directedAtGround)
        {
            CastSpellOnGround(target);
        }
        if (spellType == Spell1.Types.playerCast || spellType == Spell1.Types.passiveSwitchable)
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
        spellCasting = StartCoroutine(CastSpellRoutine(target, targetRadius, true));
    }
    private void CastSpellOnGround(RaycastHit target)
    {
        spellCasting = StartCoroutine(CastSpellRoutine(target, 0, false));
    }
    private void CastSpellSelf()
    {
        InstantiateSpell(new RaycastHit());
    }
    private Vector3 ds;
    private IEnumerator CastSpellRoutine(RaycastHit target, float targetRadius, bool isEntity)
    {
        //First we move while cast range is less then target pos
        float castRange = currentSpell.GetCastRange();
        Vector3 destinationVector = isEntity ? (EntityMath.VectorToNearestPoint(transform, target)) : (target.point - (transform.position));
        ds =  destinationVector;
        while(destinationVector.magnitude > castRange && castRange != 0)
        {
            if (isEntity)
            {
                movement.MoveToEntity(target, targetRadius);
            }
            else
            {   
                movement.MoveToPoint(target.point);
            }
            destinationVector = isEntity ? (EntityMath.VectorToNearestPoint(transform, target)) : (target.point - transform.position);
            ds = destinationVector;
            yield return null;
        }
        movement.Stop();
        movement.LookAtTarget(target, isEntity);
        InstantiateSpell(target);
        yield break;
    }

    private void InstantiateSpell(RaycastHit target)
    {
        if (currentSpell == null)
        {
            Debug.LogWarning("No spell in a slot ");
            return;
        }

        #if UNITY_EDITOR
        if (currentSpell.GetSpellType() == Spell1.Types.none)
        {
            Debug.LogWarning("Spell Type is none");
            return;
        }
        #endif

        if (currentSpell.GetSpellType() == Spell1.Types.projectile)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffect(), transform.position, transform.rotation);
            // If spell type is projectile then we move projectile
            Rigidbody rb = castEffect.AddComponent<Rigidbody>();
            rb.useGravity = false;

            //Collider so we can interract with enemy
            SphereCollider collider = castEffect.AddComponent<SphereCollider>();
            collider.radius = 0.4f;
            collider.isTrigger = true;

            //And projectile component
            ProjectileScript spellProjectileComponent = castEffect.AddComponent<ProjectileScript>();
            //FIXME: fix me later and uncomment this
            //spellProjectileComponent.spell = currentSpell;
            //spellProjectileComponent.target = target;
        }
        if (currentSpell.GetSpellType() == Spell1.Types.directedAtEnemy)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffectOnImpact(), target.transform.position, Quaternion.identity);
            if (currentSpell.HaveRadiusOnImpact)
            {
                Explosion spellExplosionComponent = castEffect.AddComponent<Explosion>();
                //spellExplosionComponent.spell = currentSpell;
            }
            GetComponent<EntityScript>().GetEntity().DamageTarget(target);
        }
        if (currentSpell.GetSpellType() == Spell1.Types.directedAtGround)
        {
            GameObject castEffect = Instantiate(currentSpell.GetEffectOnImpact(), target.point, Quaternion.identity);
            Explosion spellExplosionComponent = castEffect.AddComponent<Explosion>();
            //spellExplosionComponent.spell = currentSpell;
        }
        if (currentSpell.GetSpellType() == Spell1.Types.playerCast)
        {
            StartCoroutine(currentSpell.GetStatusEffect().StartEffect(entity));
        }
        if (currentSpell.GetSpellType() == Spell1.Types.passiveSwitchable)
        {
            currentSpell.SwitchPassive();
            entity.ApplyAllPassiveSpells();
        }
    }

    public void Stop()
    {
        if (spellCasting != null)
            StopCoroutine(spellCasting);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + ds);
    }
}
