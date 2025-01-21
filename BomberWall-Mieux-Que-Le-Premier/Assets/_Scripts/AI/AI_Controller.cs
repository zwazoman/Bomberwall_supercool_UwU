using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public event Action OnTargetReached;

    public event Action OnStartMoving;
    public event Action OnStopMoving;

    [HideInInspector] public BombHandler Bomb;

    NavMeshAgent _agent;

    private void Awake()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out Bomb);
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            OnTargetReached?.Invoke();
        }
        if(_agent.velocity.sqrMagnitude <= 1)
        {
            OnStopMoving?.Invoke();
        }
        else
        {
            OnStartMoving?.Invoke();
        }
    }

    public void MoveTo(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    public void LookTo(Vector3 destination)
    {
        transform.forward = (destination - transform.position).normalized;
    }

    public void StopMoving()
    {
        _agent.isStopped = true;
    }

    public void RestartMoving()
    {
        _agent.isStopped = false;
    }

    public void Hit()
    {
        Bomb.Hit();
    }
}
