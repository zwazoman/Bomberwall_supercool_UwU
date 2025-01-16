using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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


    [SerializeField] List<Pool> _poolsList = new List<Pool>();

    public Pool AccessPool(Pools choosenPool)
    {
        return _poolsList[(int)choosenPool];
    }
}
