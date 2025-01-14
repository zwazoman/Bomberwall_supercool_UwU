using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    /// <summary>
    /// Destroy Equivalent
    /// </summary>
    public void ReturnToPool();

    /// <summary>
    /// Start Equivalent
    /// </summary>
    public void OnPulledFromPool();

    /// <summary>
    /// OnDestroy Equivalent
    /// </summary>
    public void OnPushedToPool();
}