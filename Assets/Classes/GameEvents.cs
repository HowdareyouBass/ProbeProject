using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public struct EnemyEvents
{
    public GameEvent OnEnemyDeath;
}

public class PlayerEvents
{
    public GameEvent<float> OnPlayerDamaged = new GameEvent<float>(); // Returns damage dealt
    public GameEvent OnPlayerDeath = new GameEvent();
}
