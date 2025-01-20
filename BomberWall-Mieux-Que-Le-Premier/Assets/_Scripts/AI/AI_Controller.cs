using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public event Action OnTargetReached;

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

    public void Hit()
    {
        Bomb.Hit();
    }
}
