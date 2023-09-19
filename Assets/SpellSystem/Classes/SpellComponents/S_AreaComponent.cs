using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class S_AreaComponent : SpellComponent
{
    private enum SphereCenterPositionType { Caster, Target };

    [SerializeField] private float m_Radius;
    [SerializeField] private SphereCenterPositionType m_PositionType;


    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(CheckForEntitiesInRadius);
    }

    private void CheckForEntitiesInRadius()
    {
        if (m_Radius < 0)
            throw new System.ArgumentOutOfRangeException("Radius can't be less than 0");
        Vector3 spherePosition = Vector3.zero;
        if (m_PositionType == SphereCenterPositionType.Target)
        {
            spherePosition = target.GetPoint();
        }
        if (m_PositionType == SphereCenterPositionType.Caster)
        {
            spherePosition = caster.position;
        }

        // Later for debugging
        // GameObject gizmo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // gizmo.transform.position = spherePosition;
        // gizmo.transform.localScale = m_Radius / 100 * Vector3.one;
        // GameObject.Instantiate(gizmo);

        // Deviding by 100 because meter in game will be 1/100 of meter in Unity same in attackRange
        Collider[] colliders = Physics.OverlapSphere(spherePosition, m_Radius / 100, LayerMask.GetMask("Entity"));
        foreach (Collider collider in colliders)
        {
            // Not counting the caster collider
            if (collider == caster.GetComponent<Collider>())
            {
                continue;
            }
            if (collider.TryGetComponent<EntityScript>(out EntityScript entityScript))
            {
                ActionPerEntity(entityScript.GetEntity());
            }
            else
            {
                throw new System.Exception("The object is not Entity try changing layer or adding EntityScript component");
            }
            Debug.Log(collider.transform.name);
        }
    }

    protected abstract void ActionPerEntity(Entity entity);
}