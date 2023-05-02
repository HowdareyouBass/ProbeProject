using UnityEngine;

public class DirectedAtEnemyScript : MonoBehaviour
{
    public DirectedAtEnemy spell;
    public Transform target;
    private const int MAXIMUM_ENEMIES_IN_RADIUS = 50;

    private void Start()
    {
        if (spell.radius == 0)
        {
            Damage(target);
            return;
        }
        DamageInRadius();
    }
    private void DamageInRadius()
    {
        Collider[] hitEnemies = new Collider[MAXIMUM_ENEMIES_IN_RADIUS];
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        int numberOfEnemiesHit = Physics.OverlapSphereNonAlloc(transform.position, spell.radius, hitEnemies, enemyLayer);
        for (int i = 0; i < numberOfEnemiesHit; i++)
        {
            Damage(hitEnemies[i].transform);
        }
    }
    private void Damage(Transform enemy)
    {
        Health health = enemy.GetComponent<Health>();
        health.TakeDamage(spell.damage);
    }
    
}
