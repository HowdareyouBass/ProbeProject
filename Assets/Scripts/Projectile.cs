using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction;
    public Rigidbody rb;
    void Start()
    {
        Destroy(this.gameObject, 100f);
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.Normalize(transform.forward) * Time.deltaTime * 100);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Spell"))
            return;
        Destroy(transform.gameObject);
    }
}
