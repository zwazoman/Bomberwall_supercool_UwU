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
        _poolObject.OnPushedToPool += OnPushedToPull;
    }

    public void OnPulledFromPool()
    {
        throw new System.NotImplementedException();
    }
    

    public void OnPushedToPull()
    {
        throw new System.NotImplementedException();
    }

    public void ReturnToPool()
    {
        //return to pool
        if (_poolObject == null) Destroy(gameObject); else _poolObject.ReturnToPool();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<BombHandler>(out BombHandler bombHandler))
        {
            bombHandler.Pickup();
            ReturnToPool();
        }
    }
}
