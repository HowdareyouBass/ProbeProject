using UnityEngine;
using UnityEngine.AI;

public abstract class EntityScript : MonoBehaviour
{
    public abstract Entity GetEntity();

    public bool isDead { get; private set; }

    protected virtual void Die()
    {
        isDead = true;
        
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        Destroy(transform.gameObject, 1f);
    }
}