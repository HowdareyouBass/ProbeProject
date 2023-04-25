using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Spell spell;
    public RaycastHit target;
    public int projectileSpeed;
    public Rigidbody rb;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        projectileSpeed = spell.GetProjectileSpeed();
        //Destroy(this.gameObject, 20f);
        //if (spell.IsSelfDirected)
        //{
            transform.forward = Vector3.Normalize(target.transform.position - transform.position) * Time.deltaTime * projectileSpeed;
        //}
    }
    void FixedUpdate()
    {
        transform.forward = Vector3.Normalize(target.transform.position - transform.position) * Time.deltaTime * projectileSpeed;
        rb.velocity = transform.forward * Time.deltaTime * projectileSpeed;   
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
            Instantiate(spell.GetEffectOnImpact(), transform);
        }
        // if (collider.CompareTag("Obstacle") && !spell.IsSelfDirected)
        // {
        //     Destroy(transform.gameObject);
        //     if (spell.GetEffectOnImpact() == null)
        //     {
        //         Debug.LogWarning("Spell Impact game object isn't assigned");
        //         return;
        //     }
        //     Quaternion rot = Quaternion.LookRotation(collider.transform.position - transform.position);
        //     Instantiate(spell.GetEffectOnImpact(), transform.position, rot);
        // }
    }
}
