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

    private void Awake()
    {
        print("si �a marche faut pas que �a" + OriginPool + "soit Null");
    }

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
