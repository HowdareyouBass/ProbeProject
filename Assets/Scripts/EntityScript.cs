using RayFire;
using UnityEngine;
using UnityEngine.AI;

public abstract class EntityScript : MonoBehaviour
{
    [SerializeField] private GameObject m_NonShatter;
    [SerializeField] private GameObject m_DeathShatter;
    public abstract Entity GetEntity();

    public bool isDead { get; private set; }

    protected virtual void Die()
    {
        isDead = true;

        Destroy(GetComponent<CapsuleCollider>(), 0.1f);
        Destroy(GetComponent<Movement>(), 0.1f);
        GetComponent<NavMeshAgent>().enabled = false;
        Destroy(GetComponent<Attack>(), 0.1f);
        Destroy(GetComponent<SpellCaster>(), 0.1f);
        Invoke("DeathShatter", 2);
    }

    private void DeathShatter()
    {
        m_NonShatter.SetActive(false);
        if (m_DeathShatter == null) return;
        m_DeathShatter.SetActive(true);
        foreach (Rigidbody rb in m_DeathShatter.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(15, m_DeathShatter.transform.position, 10);
        }
        // Destroy(transform.gameObject, 3f);
    }

}