using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    Animator _animator;
    PlayerMain _main;

    private void Awake()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _main);
    }

    private void Start()
    {
        _main.Bomb.OnBombDropped += Hold;
        _main.Bomb.OnBombEquipped += Hold;

        _main.Move.OnStartMoving += Walk;
        _main.Move.OnStopMoving += Walk;

        _main.Bomb.OnThrow += Throw;

        _main.Bomb.OnHit += Hit;
    }

    void Hold()
    {
        _animator.SetBool("HasBombEquipped", _main.Bomb.HasBombsEquipped);
    }

    void Walk()
    {
        _animator.SetBool("IsMoving", _main.Move.IsMoving);
    }

    void Throw()
    {
        _animator.SetTrigger("Throw");
    }

    void Hit()
    {
        _animator.SetTrigger("Hit");
    }
}
