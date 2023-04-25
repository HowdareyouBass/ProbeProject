using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Renderer healthRenderer = new Renderer();
    public EnemyType type;
    public GameObject damageEffect;
    private EnemyStats enemy;
    [HideInInspector] public bool isDead;
    

    void Start()
    {
        enemy = new EnemyStats();
        enemy.SetType(type);
    }
    public void Damage(float amount)
    {
        GameObject effect = Instantiate(damageEffect, transform.position, transform.rotation);
        effect.transform.SetParent(transform);
        effect.GetComponent<TextMeshPro>().text = amount.ToString();

        enemy.Damage(amount);

        healthRenderer.material.SetFloat("_Health", enemy.GetCurrentHealth());

        if (enemy.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        //healthRenderer.enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<NavMeshObstacle>().enabled = false;
        Destroy(transform.gameObject, 0.1f);
    }
}
