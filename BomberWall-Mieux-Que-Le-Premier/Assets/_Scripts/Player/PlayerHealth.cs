using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;

    [SerializeField] float _deathPushForce;
    [SerializeField] float _deathTorqueForce;

    Damageable _damageable;
    Rigidbody _rb;
    PlayerInputs _inputs;

    int _currentHealth;

    private void Awake()
    {
        TryGetComponent(out _damageable);
        TryGetComponent(out _rb);
        TryGetComponent(out _inputs);
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
        if (_currentHealth == 0) Die(killer);
    }

    void Die(GameObject killer)
    {
        print("died");

        _inputs.enabled = false; // marche pas
        _rb.constraints = RigidbodyConstraints.None;
        Vector3 deathVector = transform.position - killer.transform.position;
        print(deathVector);
        _rb.AddForce(deathVector.normalized * _deathPushForce, ForceMode.Impulse);
        _rb.AddTorque(deathVector.normalized * _deathTorqueForce, ForceMode.Impulse);
    }
}
