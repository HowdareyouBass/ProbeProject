using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyEvents
{
    public GameEvent OnEnemyDeath = new GameEvent();
    public GameEvent<float> OnEnemyDamaged = new GameEvent<float>(); //Returns damage dealt
}

public class PlayerEvents
{
    public GameEvent<float> OnPlayerDamaged = new GameEvent<float>(); // Returns damage dealt
    public GameEvent OnPlayerDeath = new GameEvent();
    public GameEvent OnPlayerAttack = new GameEvent();
}
