using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        throw new System.NotImplementedException();
    }
    

    public void OnPushedToPool()
    {
        throw new System.NotImplementedException();
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
            ReturnToPool();
        }
    }
}
