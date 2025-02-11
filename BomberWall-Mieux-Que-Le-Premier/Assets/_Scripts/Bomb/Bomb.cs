using System;
using UnityEngine;

public class Bomb : MonoBehaviour,IPoolable
{
    public event Action OnBombExplode;

    [SerializeField] public float _lifeTime;
    [SerializeField] float _explosionRadius = 1;
    [SerializeField] LayerMask _explosionMask;
    [SerializeField] float _explosionPushStrength = 1;

    [HideInInspector] public float Timer = 0;

    //Poolable Initiator
    PoolObject _poolObject;
    Rigidbody _rb;
    Damageable _damageable;
    Animation _anim;

    private void Awake()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _damageable);
        TryGetComponent(out _anim);
    }

    private void Start()
    {
        TryGetComponent(out _poolObject);
        _poolObject.OnPulledFromPool += OnPulledFromPool;
        _poolObject.OnPushedToPool += OnPushedToPool;
        _damageable.OnTakeDamage += Propel;
    }

    public void OnPulledFromPool()
    {

    }

    public void OnPushedToPool()
    {
        Timer = 0;
    }

    public void ReturnToPool()
    {
        //return to pool
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }


    /// <summary>
    /// g�re l'explosion
    /// </summary>
    private void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= _lifeTime)
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
    /// pousse la bombe dans la direction donn�e
    /// </summary>
    /// <param name="direction"></param>
    public void Push(Vector3 direction)
    {
        _rb.AddForce(direction,ForceMode.Impulse);
    }

    /// <summary>
    /// envoie des spherecasts autour de la bombe ingfligeant des d�gats aux joueurs touch�s
    /// </summary>
    public void Explode()
    {
        OnBombExplode?.Invoke();

        AudioManager.Instance.PlaySFXClip(Sounds.Explosion);

        God.Instance.SummonBombPickup();
        if (transform.parent != null)
        {
            if (transform.parent.TryGetComponent(out BombHandler bombHandler))
            {
                bombHandler.BombDropped();
            }
        }
        foreach (Collider col in Physics.OverlapSphere(transform.position, _explosionRadius, _explosionMask))
        {
            if (Physics.Raycast(transform.position, col.transform.position - transform.position, _explosionRadius, LayerMask.GetMask("Wall"))) continue;
            if (col.gameObject.TryGetComponent<Damageable>(out Damageable target))
            {
                target.ApplyDamage(gameObject);
            }
        }
        ReturnToPool();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
