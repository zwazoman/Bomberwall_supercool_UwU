using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour,IPoolable
{
    [SerializeField] float _lifeTime;
    [SerializeField] float _explosionRadius = 1;
    [SerializeField] LayerMask _explosionMask;
    [SerializeField] float _explosionPushStrength = 1;

    float _timer = 0;

    //Poolable Initiator
    PoolObject _poolObject;
    Rigidbody _rb;

    private void Awake()
    {
        TryGetComponent<PoolObject>(out _poolObject);
        TryGetComponent<Rigidbody>(out _rb);
    }

    private void Start()
    {
        _poolObject.OnPulledFromPool += OnPulledFromPool;
        _poolObject.OnPushedToPool += OnPushedToPull;
    }

    public void OnPulledFromPool()
    {
        throw new System.NotImplementedException();
    }


    public void OnPushedToPull()
    {
        _timer = 0;
    }

    public void ReturnToPool()
    {
        //return to pool
        print("bomb back in pool");
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
    public void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _explosionMask);
        foreach (Collider col in hits)
        {
            if (col.gameObject.TryGetComponent<Damageable>(out Damageable target))
            {
                target.ApplyDamage(gameObject);
            }
            else
            {
                Vector3 pushVector = col.transform.position - transform.position;
                col.GetComponent<Bomb>().Push(pushVector.normalized * _explosionPushStrength);
            }
        }

        //juice

        if(transform.parent.TryGetComponent<BombHandler>(out BombHandler bombHandler))
        {
            bombHandler.BombDropped();
        }

        ReturnToPool();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
