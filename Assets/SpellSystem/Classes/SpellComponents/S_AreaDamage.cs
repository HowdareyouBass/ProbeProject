using UnityEngine;

public class S_AreaDamage : S_AreaComponent
{
    [SerializeField] private float m_Damage;

    protected override void ActionPerEntity(Entity entity)
    {
        entity.TakeDamage(m_Damage * casterEntity.Stats.Attack);
    }
}