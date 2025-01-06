using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public event Action OnPulledFromPool;
    public event Action OnPushedToPool;

    public Pool OriginPool;

    public void PulledFromPool()
    {
        OnPulledFromPool?.Invoke();
    }

    public void PushBackToPool()
    {
        OnPushedToPool?.Invoke();
        OriginPool.ReturnToPool(gameObject);
    }
}
