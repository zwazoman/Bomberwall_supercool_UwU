using System.Threading.Tasks;
using UnityEngine;

public class ExplosionVFX : MonoBehaviour, IPoolable
{
    private Bomb _bomb;
    private PoolObject _poolObject;


    void Start()
    {
        TryGetComponent(out _poolObject);
        TryGetComponent(out _bomb);
        _bomb.OnBombExplode += Boom;
        _poolObject.OnPulledFromPool += OnPulledFromPool;
        _poolObject.OnPushedToPool += OnPushedToPool;
    }

    public async void Boom()
    {
        GameObject VFX = PoolManager.Instance.AccessPool(Pools.Explosion).TakeFromPool(transform.position, Quaternion.identity);
        VFX.SetActive(true);
        await Task.Delay(1200);
        ReturnToPool();
    }

    public void OnPulledFromPool()
    {
//
    }

    public void OnPushedToPool()
    {
//
    }

    public void ReturnToPool()
    {
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }
}
