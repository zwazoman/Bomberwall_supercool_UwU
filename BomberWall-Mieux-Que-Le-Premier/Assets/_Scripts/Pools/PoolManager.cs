using System.Collections.Generic;
using UnityEngine;

public enum Pools
{
    Bomb,
    BombPickup,
    Explosion
}

/// <summary>
/// Pool Access
/// </summary>
public class PoolManager : MonoBehaviour
{
    //singleton
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Pool Manager");
                instance = go.AddComponent<PoolManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] public List<Pool> PoolsList = new List<Pool>();

    public Pool AccessPool(Pools choosenPool)
    {
        return PoolsList[(int)choosenPool];
    }
}
