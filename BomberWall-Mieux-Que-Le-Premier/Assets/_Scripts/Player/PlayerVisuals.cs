using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    Animator _animator;
    BombHandler _bombHandler;
    PlayerMain _main;
    AI_Controller _controller;

    private void Awake()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _bombHandler);
        TryGetComponent(out _main);
        TryGetComponent(out _controller);
    }

    private void Start()
    {
        _bombHandler.OnBombDropped += Hold;
        _bombHandler.OnBombEquipped += Hold;

        _bombHandler.OnThrow += Throw;

        _bombHandler.OnHit += Hit;

        if(_main != null)
        {
            _main.Move.OnStartMoving += Walk;
            _main.Move.OnStopMoving += NoWalk;
        }
        if(_controller != null)
        {
            _controller.OnStartMoving += Walk;
            _controller.OnStopMoving += NoWalk;
        }
    }

    void Hold()
    {
        _animator.SetBool("HasBombEquipped", _bombHandler.HasBombsEquipped);
    }

    void Walk()
    {
        _animator.SetBool("IsMoving", true);
    }

    void NoWalk()
    {
        _animator.SetBool("IsMoving", false);
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
