using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Spell spell;

    void Start()
    {
        Collider[] hitEnemies = new Collider[50];
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        int numberOfEnemiesHit = Physics.OverlapSphereNonAlloc(transform.position, spell.GetRadiusOnImpact(), hitEnemies, enemyLayer);
        for (int i = 0; i < numberOfEnemiesHit; i++)
        {
            Health health = hitEnemies[i].transform.GetComponent<Health>();
            //health.TakeDamage(spell.GetDamage());d
        }
    }
}