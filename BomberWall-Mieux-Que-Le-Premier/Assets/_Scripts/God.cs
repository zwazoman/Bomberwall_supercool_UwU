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

    [SerializeField] public int StartPickupCount;
    [SerializeField] public List<Transform> _bombSpawns = new List<Transform>();

    [HideInInspector] public List<Transform> BombPickups = new List<Transform>();

    public void Start()
    {
        SummonBombPickup(StartPickupCount);
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
        Transform selectedSpawn = _bombSpawns[Random.Range(0, _bombSpawns.Count)];
        SpawnBombPickup(selectedSpawn);
    }

    void SpawnBombPickup(Transform t)
    {
        GameObject bombPickup = PoolManager.Instance.AccessPool(Pools.BombPickup).TakeFromPool(t.position, Quaternion.identity);
        BombPickups.Add(bombPickup.transform);
        bombPickup.SetActive(true);
    }
}
