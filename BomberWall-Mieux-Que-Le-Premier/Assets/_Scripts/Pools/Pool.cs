using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class Pool : MonoBehaviour
{
    [SerializeField] int _poolSize;
    [SerializeField] GameObject _object;

    Queue<GameObject> _pool = new Queue<GameObject>();

    void Start()
    {
        for(int i = 0; i < _poolSize; i++)
        {
            AddNewObjectToPool();
        }
    }

    /// <summary>
    /// créé un objet, lui rajoute le componenet PoolObject et le rajoute à la pool
    /// </summary>
    /// <returns></returns>
    GameObject AddNewObjectToPool()
    {
        GameObject pooledObject = Instantiate(_object);
        PoolObject poolObject;
        if (_object.TryGetComponent<PoolObject>(out PoolObject pObject))
        {
            poolObject = pObject;
        }
        else
        {
            poolObject = pooledObject.AddComponent<PoolObject>();
        }
        poolObject.OriginPool = this;
        ReturnToPool(pooledObject);
        return pooledObject;
    }

    /// <summary>
    /// returns an object to its pool
    /// </summary>
    /// <param name="objectToReturn"></param>
    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.transform.parent = transform;
        objectToReturn.transform.position = Vector3.zero;
        objectToReturn.SetActive(false);
        _pool.Enqueue(objectToReturn);
    }

    /// <summary>
    /// returns an object removed from the pool and activated
    /// </summary>
    /// <returns></returns>
    public GameObject TakeFromPool(Vector3 pos, Quaternion rot)
    {
        GameObject poolObject;
        if (_pool.Count == 0)
        {
            poolObject = AddNewObjectToPool();
        }
        else
        {
            poolObject = _pool.Dequeue();
        }
        poolObject.transform.position = pos;
        poolObject.transform.rotation = rot;
        poolObject.SetActive(true);
        poolObject.GetComponent<PoolObject>().PulledFromPool();
        return poolObject;
    }

    /// <summary>
    /// acces a un objet de la pool (si la pool est vide : une nouvelle instance est créée et rajoutée à la pool
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject TakeFromPool(Transform parent)
    {
        GameObject poolObject;
        if (_pool.Count == 0)
        {
            poolObject = AddNewObjectToPool();
        }
        else
        {
            poolObject = _pool.Dequeue();
        }
        poolObject.transform.parent = parent;
        poolObject.SetActive(true);
        poolObject.GetComponent<PoolObject>().PulledFromPool();
        return poolObject;
    }

}
