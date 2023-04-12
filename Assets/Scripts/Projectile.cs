using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Spell spell;
    public int projectileSpeed;
    public Rigidbody rb;
    public Vector3 targetRotation;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        projectileSpeed = spell.GetSpeed();
        Destroy(this.gameObject, 100f);
    }
    void FixedUpdate()
    {
        rb.velocity = Vector3.Normalize(transform.forward) * Time.deltaTime * projectileSpeed;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.transform.GetComponent<EnemyBehavior>().Damage(spell.GetDamage());
            Destroy(transform.gameObject);
            if (spell.GetEffectOnImpact() == null)
            {
                Debug.LogWarning("Spell Impact game object isn't assigned");
                return;
            }
            Instantiate(spell.GetEffectOnImpact());
        }
    }
}
