using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour,IPoolable
{
    public event Action OnBombExplode;

    [SerializeField] float _lifeTime;
    [SerializeField] float _explosionRadius = 1;
    [SerializeField] LayerMask _explosionMask;
    [SerializeField] float _explosionPushStrength = 1;

    [SerializeField] GameObject _explode; 

    float _timer = 0;

    //Poolable Initiator
    PoolObject _poolObject;
    Rigidbody _rb;
    Damageable _damageable;
    Animation _anim;

    private void Awake()
    {
        TryGetComponent(out _poolObject);
        TryGetComponent(out _rb);
        TryGetComponent(out _damageable);
        TryGetComponent(out _anim);
    }

    private void Start()
    {
        _poolObject.OnPulledFromPool += OnPulledFromPool;
        _poolObject.OnPushedToPool += OnPushedToPool;
        _damageable.OnTakeDamage += Propel;
    }

    public void OnPulledFromPool()
    {
        print("chien");
    }

    public void OnPushedToPool()
    {
        print("singe");
        _timer = 0;
    }

    public void ReturnToPool()
    {
        //return to pool
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }


    /// <summary>
    /// gère l'explosion
    /// </summary>
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _lifeTime)
        {
            Explode();
        }
    }

    void Propel(GameObject killer)
    {
        Vector3 propelVector = transform.position - killer.transform.position;
        Push(propelVector * _explosionPushStrength);
    }

    /// <summary>
    /// pousse la bombe dans la direction donnée
    /// </summary>
    /// <param name="direction"></param>
    public void Push(Vector3 direction)
    {
        _rb.AddForce(direction,ForceMode.Impulse);
    }

    /// <summary>
    /// envoie des spherecasts autour de la bombe ingfligeant des dégats aux joueurs touchés
    /// </summary>
    public async void Explode()
    {
        OnBombExplode?.Invoke();
        God.Instance.SummonBombPickup();
        if (transform.parent != null)
        {
            if (transform.parent.TryGetComponent(out BombHandler bombHandler))
            {
                bombHandler.BombDropped();
            }
        }
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _explosionMask);
        foreach (Collider col in hits)
        {
            if (col.gameObject.TryGetComponent<Damageable>(out Damageable target))
            {
                target.ApplyDamage(gameObject);
            }
        }

        /*Debug.Log("test");
        _explode.SetActive(true);
        await Task.Delay(2000);*/

        ReturnToPool();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
