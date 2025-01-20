using System;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Sensor : MonoBehaviour
{
    public event Action<GameObject> OnBombNear;
    public event Action<GameObject> OnBombVeryNear;
    public event Action OnBombFar;
    public event Action OnPlayerInRange;

    [Header("Parameters")]
    [SerializeField] float _bombDetectionrange = 1;
    [SerializeField] float _playerDetectionRange = 1;
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
        Collider[] hits = Physics.OverlapSphere(transform.position, _bombDetectionrange, _sensorMask);
        foreach (var item in hits)
        {
            if(item.gameObject.TryGetComponent<Bomb>(out Bomb bomb))
            {
                float bombDistance = (item.transform.position - transform.position).magnitude;
                if (bombDistance <= _bombDetectionrange / 2) OnBombVeryNear?.Invoke(bomb.gameObject); else OnBombNear?.Invoke(bomb.gameObject);
            }
            else
            {
                OnBombFar?.Invoke();
            }
        }
    }

    public Transform GetClosestPickup()
    {
        Transform closest = God.Instance._bombPickups[0];
        foreach(Transform pickup in God.Instance._bombPickups)
        {
            Vector3 playerToClosest = closest.position - transform.position;
            Vector3 playerToPickup = pickup.position - transform.position;
            if(playerToPickup.sqrMagnitude < playerToClosest.sqrMagnitude)
            {
                closest = pickup;
            }
        }
        return closest;
    }

    public GameObject GetClosestPlayer()
    {
        GameObject closest = UIManager.Instance.Players[0];
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

}
