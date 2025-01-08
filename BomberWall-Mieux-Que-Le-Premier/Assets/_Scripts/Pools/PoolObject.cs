using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pool component
/// </summary>
public class PoolObject : MonoBehaviour
{
    public event Action OnPulledFromPool;
    public event Action OnPushedToPool;

    public Pool OriginPool;

    public void ReturnToPool()
    {
        OriginPool.ReturnToPool(gameObject);
    }

    public void PulledFromPool()
    {
        OnPulledFromPool?.Invoke();
    }

    public void PushedToPull()
    {
        OnPushedToPool?.Invoke();
        OriginPool.ReturnToPool(gameObject);
    }
}
