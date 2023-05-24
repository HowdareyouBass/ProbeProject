using UnityEngine;

[RequireComponent(typeof(SpellScript))]
[RequireComponent(typeof(ActiveSpell))]
public class ProjectileScript : SpellComponent1
{
    [SerializeField] private bool m_IsHoming = true;
    [SerializeField] private float m_Damage = 10;
    [SerializeField] private GameObject m_EffectOnImpact;
    [SerializeField] private int m_Speed = 100;

    private Rigidbody m_RigidBody;
    private GameEvent m_OnImpact;

    private void Start()
    {
        m_OnImpact = spellScript.events.GetEvent(SpellEventName.OnImpact);

        if (!TryGetComponent<Rigidbody>(out m_RigidBody))
        {
            m_RigidBody = gameObject.AddComponent<Rigidbody>();
            m_RigidBody.useGravity = false;
        }
        if (!TryGetComponent<SphereCollider>(out SphereCollider collider))
        {
            collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 0.4f;
            collider.isTrigger = true;
        }
    }
    private void FixedUpdate()
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
            health.TakeDamage(m_Damage);
            Destroy(gameObject);
            Instantiate(m_EffectOnImpact, collider.ClosestPoint(transform.position), Quaternion.identity);
        }
    }
}