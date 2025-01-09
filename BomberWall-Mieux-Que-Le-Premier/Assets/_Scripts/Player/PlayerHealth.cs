using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;

    int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        _currentHealth--;
        //juice
        if (_currentHealth < _maxHealth) Die();
    }

    void Die()
    {
        //mourir
        //juice
    }
}
