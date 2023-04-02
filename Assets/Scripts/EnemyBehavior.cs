using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Renderer healthRenderer = new Renderer();
    public EnemyType type;
    public TextMeshProUGUI enemyHelth;
    public GameObject damageEffect;
    private EnemyStats enemy;
    

    void Start()
    {
        enemy = new EnemyStats();
        enemy.SetType(type);
    }
    void Update()
    {
        enemyHelth.text = enemy.GetCurrentHealth().ToString();
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
        Destroy(transform.gameObject, 0);
    }
}
