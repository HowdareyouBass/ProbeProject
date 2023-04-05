using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Spell spell;
    public int spellSlot;
    public Rigidbody rb;
    void Start()
    {
        Destroy(this.gameObject, 100f);
    }
    void FixedUpdate()
    {
        rb.velocity = Vector3.Normalize(transform.forward) * Time.deltaTime * 100;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.transform.GetComponent<EnemyBehavior>().Damage(spell.Damage);
            Destroy(transform.gameObject);
        }
    }
}
