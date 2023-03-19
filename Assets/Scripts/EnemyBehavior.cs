using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyType type;
    private EnemyStats enemy;
    public TextMeshProUGUI enemyHelth;
    

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
        enemy.Damage(amount);
    }
}
