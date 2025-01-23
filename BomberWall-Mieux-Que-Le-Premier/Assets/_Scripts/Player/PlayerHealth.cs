using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnDamageTook;
    public event Action OnDeath;

    [SerializeField] public int Maxhealth;

    [SerializeField] float _deathPushForce;
    [SerializeField] float _deathTorqueForce;

    Damageable _damageable;
    Rigidbody _rb;

    public int CurrentHealth { get; set; }

    private void Awake()
    {
        TryGetComponent(out _damageable);
        TryGetComponent(out _rb);

        CurrentHealth = Maxhealth;
    }

    private void Start()
    {
        _damageable.OnTakeDamage += TakeDamage;

        AudioManager.Instance.PlaySFXClip(Sounds.Spawn);
    }

    public void TakeDamage(GameObject killer)
    {
        CurrentHealth--;
        //juice
        OnDamageTook?.Invoke();

        AudioManager.Instance.PlaySFXClip(Sounds.Damage);

        if (CurrentHealth == 0) Die(killer);
    }

    void Die(GameObject killer)
    {
        print("died");


        if(TryGetComponent(out PlayerMove playerMove)) playerMove.CanMove = false;
        if(TryGetComponent(out NavMeshAgent agent)) agent.speed = 0;

        _rb.constraints = RigidbodyConstraints.None;
        _rb.AddForce(Vector3.up * _deathPushForce, ForceMode.Impulse);
        _rb.AddTorque(transform.forward * _deathTorqueForce, ForceMode.Impulse);
        UIManager.Instance.Players.Remove(gameObject);

        AudioManager.Instance.PlaySFXClip(Sounds.Death);

        OnDeath?.Invoke();
    }
}
