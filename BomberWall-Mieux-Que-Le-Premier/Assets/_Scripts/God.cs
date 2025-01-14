using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class God : MonoBehaviour
{
    //singleton
    private static God instance;

    public static God Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("God");
                instance = go.AddComponent<God>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] int _bombCount;
    [SerializeField] Transform[] _bombSpawns;

    private void Start()
    {
        SummonBombPickup(_bombCount);
    }

    void SummonBombPickup(int x)
    {
        List<Transform> availableSpawns = new List<Transform>();
        foreach (Transform t in _bombSpawns)
        {
            availableSpawns.Add(t);
        }

        for (int i = 0; i<x; i++)
        {
            Transform selectedSpawn = availableSpawns[Random.Range(0, availableSpawns.Count)];
            SpawnBombPickup(selectedSpawn);
            availableSpawns.Remove(selectedSpawn);
        }
    }

    void SummonBombPickup()
    {
        Transform selectedSpawn = _bombSpawns[Random.Range(0, _bombSpawns.Length)];
        SpawnBombPickup(selectedSpawn);
    }

    void SpawnBombPickup(Transform t)
    {
        Pool pickupPool = PoolManager.Instance.AccessPool(Pools.BombPickup);
        pickupPool.TakeFromPool(transform.position, Quaternion.identity);
    }
}
