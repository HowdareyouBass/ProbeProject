using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float TravelSpeed;

    private Collider m_Collider;
    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }
}