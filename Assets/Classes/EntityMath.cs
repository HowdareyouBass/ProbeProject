using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityMath
{
    public static Vector3 VectorToNearestPoint(Transform original, RaycastHit target)
    {
        float originalRadius;
        if (original.CompareTag("Player"))
        {
            originalRadius = Player.PLAYER_RADIUS;
        }
        else
        {
            originalRadius = original.GetComponent<Collider>().bounds.size.z;
        }
        Vector3 originalPosition = original.position;
        float targetRadius = target.collider.bounds.size.z;
        Vector3 targetPosition = target.transform.position;

        if (targetRadius == 0)
        {
            return targetPosition - originalPosition;
        }
        Vector3 toOriginalFromTargetNormalized = Vector3.Normalize(originalPosition - targetPosition) / 2;
        return (targetPosition - originalPosition) + toOriginalFromTargetNormalized * (targetRadius + originalRadius);
    }
}
