using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesHealth : MonoBehaviour, IHaveHealth
{
    public event Action OnDie = delegate { };
    [SerializeField]
    float _maxHealth;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set 
        { 
            _maxHealth = value;
            CurrentHealth = value;
        }
    }
    [SerializeField]
    float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set 
        {
            if (value <= 0) { Die();  }
            else if (value > MaxHealth) _currentHealth = MaxHealth;
            else _currentHealth = value; 
        }
    }
    public void GetDamage(float dmg)
    {
        CurrentHealth -= dmg;
    }
    public void Heal(float amount)
    {
        CurrentHealth += amount;
    }

    public void Die()
    {
        OnDie?.Invoke();
    }

}
