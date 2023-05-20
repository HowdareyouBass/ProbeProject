using UnityEngine;

public class EntityMath
{
    public static Vector3 VectorToNearestPoint(Transform original, Target target)
    {
        float originalRadius = original.GetComponent<Collider>().bounds.size.z;
        Vector3 originalPosition = original.transform.position;
        Vector3 targetPosition = target.transform.position;
        Vector3 toOriginalFromTargetNormalized = Vector3.Normalize(originalPosition - targetPosition) / 2;
        return (targetPosition - originalPosition) + toOriginalFromTargetNormalized * (target.radius /*+ originalRadius*/);
    }
}