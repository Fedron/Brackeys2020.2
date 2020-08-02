using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth
{
    event Action OnDie;
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    void GetDamage(float dmg);
    void Heal(float amount);
    void Die();
    
}
