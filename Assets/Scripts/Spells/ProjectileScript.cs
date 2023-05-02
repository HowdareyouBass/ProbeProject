using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Projectile spell;
    public Transform target;
    [SerializeField] private int projectileSpeed;
    private Rigidbody rb;
    private Vector3 targetRotation;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        projectileSpeed = spell.projectileSpeed;
    }
    void FixedUpdate()
    {
        transform.forward = Vector3.Normalize(target.position - transform.position) * Time.deltaTime * projectileSpeed;
        rb.velocity = transform.forward * Time.deltaTime * projectileSpeed;   
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.transform.GetComponent<Health>().TakeDamage(spell.damage);
            Destroy(transform.gameObject);
            if (spell.effectOnImpact == null)
            {
                Debug.LogWarning("Spell Impact game object isn't assigned");
                return;
            }
            Instantiate(spell.effectOnImpact, transform);
        }
    }
}
