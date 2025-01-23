using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_Sensor : MonoBehaviour
{
    public event Action<GameObject> OnBombNear;
    public event Action<GameObject> OnBombVeryNear;
    public event Action OnBombFar;
    public event Action OnPlayerNear;

    [HideInInspector] public bool PlayerNear = false;

    [Header("Parameters")]
    [SerializeField] public float BombDetectionrange = 1;
    [SerializeField] float _playerDetectionRange = 1;
    [SerializeField] float _kamikazeRangeDivider = 1;
    [SerializeField] LayerMask _sensorMask;

    PlayerHealth _health;
    BombHandler _bombHandler;
    AI_Controller _controller;

    private void Awake()
    {
        TryGetComponent(out _health);
        TryGetComponent(out _bombHandler);
        TryGetComponent(out _controller);
    }

    private void Update()
    {
        foreach (Collider _coll in Physics.OverlapSphere(transform.position, _playerDetectionRange, _sensorMask))
        {
            //if (_coll.gameObject == gameObject) return;

            float distanceToObject = (_coll.transform.position - transform.position).magnitude;

            if(_coll.gameObject.TryGetComponent<BombHandler>(out BombHandler bombHandler) && _coll.gameObject != gameObject)
            {
                PlayerNear = true;
                OnPlayerNear?.Invoke();

            } else PlayerNear = false;


            if (_coll.gameObject.TryGetComponent<Bomb>(out Bomb bomb) && distanceToObject <= BombDetectionrange && bomb.Timer >= 1.5f)
            {
                OnBombNear?.Invoke(bomb.gameObject);
                //if (distanceToObject <= BombDetectionrange / _kamikazeRangeDivider) OnBombVeryNear?.Invoke(bomb.gameObject); else OnBombNear?.Invoke(bomb.gameObject);
            }
        }
    }

    public Transform GetClosestPickup()
    {
        if(God.Instance.BombPickups.Count == 0) return null;

        Transform closest = God.Instance.BombPickups[0];
        foreach(Transform pickup in God.Instance.BombPickups)
        {
            Vector3 playerToClosest = closest.position - transform.position;
            Vector3 playerToPickup = pickup.position - transform.position;
            if(playerToPickup.sqrMagnitude <= playerToClosest.sqrMagnitude)
            {
                closest = pickup;
            }
        }
        return closest;
    }

    public GameObject GetClosestPlayer()
    {
        List<GameObject> goodPlayerList = new List<GameObject>();
        goodPlayerList = UIManager.Instance.Players;

        goodPlayerList.Remove(gameObject);

        GameObject closest = goodPlayerList[0];
        foreach (GameObject player in UIManager.Instance.Players)
        {
            Vector3 playerToClosest = closest.transform.position - transform.position;
            Vector3 playerToPickup = player.transform.position - transform.position;
            if (playerToPickup.sqrMagnitude < playerToClosest.sqrMagnitude)
            {
                closest = player;
            }
        }
        return closest;
    }

    public Vector3 GetClosestNavmeshPoint(Vector3 destination)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, 4,NavMesh.AllAreas);

        return hit.position;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, BombDetectionrange);
        Gizmos.DrawWireSphere(transform.position, BombDetectionrange / _kamikazeRangeDivider);
        Gizmos.DrawWireSphere(transform.position, _playerDetectionRange);
    }

}
