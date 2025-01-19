using UnityEngine;

public class BombPickup : MonoBehaviour,IPoolable
{    
    //Poolable Initiator
    PoolObject _poolObject;

    private void Awake()
    {
        TryGetComponent<PoolObject>(out _poolObject);
    }

    private void Start()
    {
        _poolObject.OnPulledFromPool += OnPulledFromPool;
        _poolObject.OnPushedToPool += OnPushedToPool;
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
        //return to pool
        if (_poolObject == null) Destroy(gameObject); else _poolObject.PushToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<BombHandler>(out BombHandler bombHandler))
        {
            bombHandler.Pickup();
            God.Instance._bombPickups.Remove(transform);
            ReturnToPool();
        }
    }
}
