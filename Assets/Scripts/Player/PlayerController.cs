using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField] private GameObject m_MovementEffect;

    public override void OnMouseClick(Target target)
    {
        SpawnEffect(target.GetPoint(), target.normal);
        base.OnMouseClick(target);
    }

    private void SpawnEffect(Vector3 position, Vector3 rotation)
    {
        if (m_MovementEffect != null)
            Instantiate(m_MovementEffect, position, Quaternion.LookRotation(rotation));
    }
}