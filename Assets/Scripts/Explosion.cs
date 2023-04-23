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
            EnemyBehavior enemy = hitEnemies[i].transform.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.Damage(spell.GetDamage());
            }
            else
            {
                Debug.LogError("No EnemyBehavior component found on enemy. Try using prefab");
            }
        }
    }
}
