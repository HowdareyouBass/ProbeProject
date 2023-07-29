using UnityEngine;

public class Target
{
    public Transform transform { get; private set; }
    public Vector3 normal { get; private set; } = Vector3.up;
    public Entity TargetEntity => transform.GetComponent<EntityScript>().GetEntity();

    public bool isEntity { get => transform.TryGetComponent<EntityScript>(out var a); }
    public float height { get => m_Collider.bounds.size.y; }
    public float radius { get => m_Collider.bounds.size.x; }

    private Collider m_Collider;
    private Vector3 m_Position;

    public Target(Transform target, Vector3 point, Collider collider)
    {
        transform = target;
        m_Collider = collider;
        m_Position = point;
    }
    public Target(RaycastHit hit)
    {
        transform = hit.transform;
        m_Collider = hit.collider;
        m_Position = hit.point;
        normal = isEntity ? Vector3.up : hit.normal;
    }

    //Returns vector to target
    public Vector3 GetVector(Transform from)
    {
        if (isEntity)
        {
            return EntityMath.VectorToNearestPoint(from, this);
        }
        else
        {
            return m_Position - from.position;
        }
    }
    public Vector3 GetPoint()
    {
        if (isEntity)
        {
            Vector3 result = transform.position;
            result.y -= height/2;
            return result;
        }
        else
        {
            return m_Position;
        }
    }
}
