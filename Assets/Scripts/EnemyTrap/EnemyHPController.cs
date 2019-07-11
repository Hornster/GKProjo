using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information about object HP and updates it over time
/// </summary>
class EnemyHPController : MonoBehaviour
{
    /// <summary>
    /// Maximum value of object HP
    /// </summary>
    public float HPMax;

    /// <summary>
    /// Current HP value. Minimum value is 0f. Object with this hp value is considered dead
    /// </summary>
    float _currentHP;
    
    public void Awake()
    {
        _currentHP = HPMax; //Start with max HP
    }
    
    public void ApplyDamage(float damage)
    {
        _currentHP -= damage;
    }


    public bool IsDead()
    {
        if (_currentHP <= 0f)
            return true;
        return false;
    }

    public bool IsAlive()
    {
        if (_currentHP > 0f)
            return true;
        return false;
    }
}
