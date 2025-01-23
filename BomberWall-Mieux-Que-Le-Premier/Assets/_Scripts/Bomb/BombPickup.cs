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
            if (God.Instance.BombPickups.Contains(transform))
            {
                God.Instance.BombPickups.Remove(transform);
            }

            AudioManager.Instance.PlaySFXClip(Sounds.Spawn);

            bombHandler.Pickup();
            ReturnToPool();
        }
    }
}
