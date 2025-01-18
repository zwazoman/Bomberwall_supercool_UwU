using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnTakeDamage;
    public event Action OnDeath;

    [SerializeField] int _maxHealth;

    [SerializeField] float _deathPushForce;
    [SerializeField] float _deathTorqueForce;
    [SerializeField] private ParticleSystem _test;

    Damageable _damageable;
    Rigidbody _rb;
    PlayerInputs _inputs;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        TryGetComponent(out _damageable);
        TryGetComponent(out _rb);
        TryGetComponent(out _inputs);
        CurrentHealth = _maxHealth;
    }

    private void Start()
    {
        _damageable.OnTakeDamage += TakeDamage;
    }

    public void TakeDamage(GameObject killer)
    {
        CurrentHealth--;
        print("damage took");
        //juice
        OnTakeDamage?.Invoke();
        _test.Play();
        if (CurrentHealth == 0) Die(killer);
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
        UIManager.Instance.Players.Remove(gameObject);
        OnDeath?.Invoke();
    }
}
