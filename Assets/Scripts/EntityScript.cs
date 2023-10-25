using UnityEngine;
using UnityEngine.AI;

public abstract class EntityScript : MonoBehaviour
{
    public abstract Entity GetEntity();

    public bool isDead { get; private set; }

    protected virtual void Die()
    {
        isDead = true;
        
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Movement>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        // GetComponent<MeshRenderer>().enabled = false;
        Destroy(transform.gameObject, 1f);
    }
}