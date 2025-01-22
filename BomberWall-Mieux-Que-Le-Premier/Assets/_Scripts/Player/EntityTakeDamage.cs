using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityTakeDamage : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    private PlayerHealth _playerHealth;

    private void Start()
    {
        TryGetComponent<PlayerHealth>(out _playerHealth);
        _playerHealth.OnDamageTook += OnHit;

    }
    public void OnHit()
    {
        ParticleSystem.Play();
    }
}
