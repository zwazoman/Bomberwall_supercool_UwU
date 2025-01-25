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
        if (OriginPool == null) print("Y'A UN SOUCIS LA");
        OnPulledFromPool?.Invoke();
    }

    public void PushToPool()
    {
        OnPushedToPool?.Invoke();
        OriginPool.ReturnToPool(gameObject);
    }
}
