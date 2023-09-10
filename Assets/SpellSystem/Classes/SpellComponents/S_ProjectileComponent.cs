using UnityEngine;

public class S_ProjectileComponent : SpellComponent
{
    [SerializeField] private GameObject m_ProjectileObject;    

    public override void Init()
    {
        spell.events.GetEvent(SpellEventName.OnCast).Subscribe(SpawnProjectile);
    }

    private void SpawnProjectile()
    {
        GameObject projectileObject = GameObject.Instantiate(m_ProjectileObject);
        
    }
}