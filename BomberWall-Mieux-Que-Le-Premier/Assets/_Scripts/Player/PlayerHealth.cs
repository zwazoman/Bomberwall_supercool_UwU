using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    Damageable damageable;

    int _currentHealth;

    private void Awake()
    {
        TryGetComponent<Damageable>(out damageable);
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        damageable.OnTakeDamage += TakeDamage;
    }

    public void TakeDamage()
    {
        _currentHealth--;
        print("damage took");
        //juice
        if (_currentHealth < _maxHealth) Die();
    }

    void Die()
    {
        print("died");
        //mourir
        //juice
    }
}
