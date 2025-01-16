using UnityEngine;

public class ExplosionVFX : MonoBehaviour, IPoolable
{
    private Bomb _bomb;
    private PoolObject _poolObject;


    void Awake()
    {
        TryGetComponent(out _poolObject);
        TryGetComponent(out _bomb);
        _bomb.OnBombExplode += Boom;
        _poolObject.OnPulledFromPool += OnPulledFromPool;
        _poolObject.OnPushedToPool += OnPushedToPool;
    }

    public void Boom()
    {

    }

    public void OnPulledFromPool()
    {
        throw new System.NotImplementedException();
    }

    public void OnPushedToPool()
    {
        throw new System.NotImplementedException();
    }

    public void ReturnToPool()
    {
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }
}
