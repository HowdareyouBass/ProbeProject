using System;
using UnityEngine;

public class S_ProjectileComponent : SpellComponent
{

    [SerializeField] private float m_TravelSpeed;
    [SerializeField] private float m_DecayTimeInSeconds;
    [SerializeField] private float m_Damage;
    [SerializeField] private bool m_DecayOnCollision;
    [SerializeField] private GameObject m_ProjectileObject;
    [SerializeField] private bool m_StickToFloor;

    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(SpawnProjectile);
    }

    private void SpawnProjectile()
    {
        GameObject projectileObject = GameObject.Instantiate(m_ProjectileObject);
        projectileObject.transform.position = target.GetPoint();

        Projectile projectileComponent = projectileObject.GetComponent<Projectile>();
        projectileComponent.TravelSpeed = m_TravelSpeed;
        projectileComponent.Direction = Vector3.Normalize(target.GetPoint() - caster.position);
        projectileComponent.DecayTimeInSeconds = m_DecayTimeInSeconds;
        projectileComponent.Damage = m_Damage * casterEntity.Stats.Attack;
        projectileComponent.DecayOnCollision = m_DecayOnCollision;
    }
}