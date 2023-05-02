using UnityEngine;

[System.Serializable]
public class Projectile : Spell, IDamaging, ICastableOnEntity, IHasEffectOnImpact
{
    [SerializeField] private float m_Damage;
    [SerializeField] private int m_ProjectileSpeed = 100;
    [SerializeField] private GameObject m_EffectOnImpact = null;

    public float damage { get => m_Damage; }
    public GameObject effectOnImpact { get => m_EffectOnImpact; }
    public int projectileSpeed { get => m_ProjectileSpeed; }

    private Transform m_Target;

    public void Cast(Transform caster, Transform target)
    {
        SetTarget(target);
        InitEffect(caster);
    }
    private void InitEffect(Transform transform)
    {
        GameObject castEffect = GameObject.Instantiate(effect, transform.position, transform.rotation);

        // If spell type is projectile then we move projectile
        Rigidbody rigidBody = castEffect.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;

        //Collider so we can interract with enemy
        SphereCollider collider = castEffect.AddComponent<SphereCollider>();
        collider.radius = 0.4f;
        collider.isTrigger = true;

        //And projectile component
        ProjectileScript spellProjectileComponent = castEffect.AddComponent<ProjectileScript>();
        spellProjectileComponent.spell = this;
        spellProjectileComponent.target = m_Target;
    }
    private void SetTarget(Transform targetTransform)
    {  
        m_Target = targetTransform;
    }
}
