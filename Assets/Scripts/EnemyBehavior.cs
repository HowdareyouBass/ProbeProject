using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
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
        Vector3 offset = new Vector3(0f, 10000f, 0f);
        GameObject effect = Instantiate(damageEffect, transform.position + offset, transform.rotation);
        effect.transform.SetParent(transform);
        effect.GetComponent<TextMeshPro>().text = amount.ToString();
        enemy.Damage(amount);
    }
}
