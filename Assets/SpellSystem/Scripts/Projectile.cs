using System.Collections;
using System.Threading;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    // Must define these before Instantiating object
    public float TravelSpeed;
    public Vector3 Direction;
    public float DecayTimeInSeconds;
    public float Damage;
    public bool DecayOnCollision = false;

    private Collider m_Collider;
    private Rigidbody m_Rigidbody;
    private Coroutine m_DecayCoroutine;

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Direction.y = 0;
        m_Rigidbody.velocity = Direction * TravelSpeed;
        transform.rotation = Quaternion.LookRotation(Direction);
        m_DecayCoroutine = StartCoroutine(DecayTimer());
    }

    private IEnumerator DecayTimer()
    {
        float timer = 0f;

        while(timer < DecayTimeInSeconds)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EntityScript>(out EntityScript entityScript))
        {
            entityScript.GetEntity().TakeDamage(Damage);
            if (DecayOnCollision)
            {
                StopCoroutine(m_DecayCoroutine);
                Destroy(gameObject);
            }
        }
    }
}