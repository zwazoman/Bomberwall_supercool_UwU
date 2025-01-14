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

    private void Awake()
    {
        print("si ça marche faut pas que ça" + OriginPool + "soit Null");
    }

    public void PullFromPool()
    {
        print("connard ptn");
        OnPulledFromPool?.Invoke();
    }

    public void PushToPool()
    {
        OnPushedToPool?.Invoke();
        print(OriginPool);
        OriginPool.ReturnToPool(gameObject);
    }
}
