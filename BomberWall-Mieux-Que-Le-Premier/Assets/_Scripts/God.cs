using System.Collections.Generic;
using UnityEngine;

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

    public List<Transform> _bombPickups = new List<Transform>();

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
            availableSpawns.Remove(selectedSpawn);
            SpawnBombPickup(selectedSpawn);
        }
    }

    public void SummonBombPickup()
    {
        Transform selectedSpawn = _bombSpawns[Random.Range(0, _bombSpawns.Length)];
        SpawnBombPickup(selectedSpawn);
    }

    void SpawnBombPickup(Transform t)
    {
        _bombPickups.Add(t);
        GameObject bomb = PoolManager.Instance.AccessPool(Pools.BombPickup).TakeFromPool(t.position, Quaternion.identity);
        bomb.SetActive(true);
    }
}
