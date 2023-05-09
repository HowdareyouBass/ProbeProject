using UnityEngine;

[RequireComponent(typeof(SpellScript))]
[RequireComponent(typeof(ActiveSpell))]
public class ProjectileScript : SpellComponent
{
    [SerializeField] private bool m_IsHoming = true;
    [SerializeField] private bool m_CanDamage = true;
    [SerializeField] private float m_Damage = 10;
    [SerializeField] private GameObject m_EffectOnImpact;
    [SerializeField] private int m_Speed = 100;

    private Rigidbody m_RigidBody;
    private GameEvent m_OnImpact;

    private void Start()
    {
        m_OnImpact = spellScript.GetEvent(SpellEventName.OnImpact);

        SphereCollider collider;
        if (!TryGetComponent<Rigidbody>(out m_RigidBody))
        {
            m_RigidBody = gameObject.AddComponent<Rigidbody>();
            m_RigidBody.useGravity = false;
        }
        if (!TryGetComponent<SphereCollider>(out collider))
        {
            collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 0.4f;
            collider.isTrigger = true;
        }
    }
    void FixedUpdate()
    {
        if (m_IsHoming)
        {
            transform.forward = target.position - transform.position;
        }
        m_RigidBody.velocity = transform.forward * Time.deltaTime * m_Speed;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.TryGetComponent<Health>(out Health health) && collider.transform != caster)
        {
            m_OnImpact.Trigger();
            if (m_CanDamage)
            {
                health.TakeDamage(m_Damage);
            }
            Destroy(transform.gameObject);
            Instantiate(m_EffectOnImpact, collider.ClosestPoint(transform.position), Quaternion.identity);
        }
    }
}