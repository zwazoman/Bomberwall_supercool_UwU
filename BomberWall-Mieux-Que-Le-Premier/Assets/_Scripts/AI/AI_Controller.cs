using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI_Controller : MonoBehaviour
{
    public event Action OnTargetReached;

    NavMeshAgent _agent;
    BombHandler _bombHandler;

    private void Awake()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out _bombHandler);
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

    public void Hit()
    {
        _bombHandler.Hit();
    }
}
