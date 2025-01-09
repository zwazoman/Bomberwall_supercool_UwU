using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;

    [SerializeField] float _deathPushForce;

    Damageable _damageable;
    Rigidbody _rb;

    int _currentHealth;

    private void Awake()
    {
        TryGetComponent(out _damageable);
        TryGetComponent(out _rb);
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _damageable.OnTakeDamage += TakeDamage;
    }

    public void TakeDamage(GameObject killer)
    {
        _currentHealth--;
        print("damage took");
        //juice
        if (_currentHealth < _maxHealth) Die(killer);
    }

    void Die(GameObject killer)
    {
        print("died");
        Vector3 deathVector = killer.transform.position - transform.position;
        _rb.AddForce(deathVector.normalized * _deathPushForce, ForceMode.Impulse);
        //mourir
        //juice
    }
}
