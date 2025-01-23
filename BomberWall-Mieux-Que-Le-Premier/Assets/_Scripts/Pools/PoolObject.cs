using System;
using UnityEngine;

/// <summary>
/// Pool component
/// </summary>
public class PoolObject : MonoBehaviour
{
    public event Action OnPulledFromPool;
    public event Action OnPushedToPool;

    public Pool OriginPool;

    public void PullFromPool()
    {
        OnPulledFromPool?.Invoke();
    }

    public void PushToPool()
    {
        OnPushedToPool?.Invoke();
        print(OriginPool);
        OriginPool.ReturnToPool(gameObject);
    }
}
