using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHaveHealth
{
    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }
    void GetDamage();
    void Heal();
    void Die();
    
}
