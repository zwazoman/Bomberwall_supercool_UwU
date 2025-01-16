using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_BaseState
{
    public AI_StateMachine StateMachine;

    public abstract void OnEnter();
    public abstract void Update();
    public abstract void OnExit();

    protected void EnterChase()
    {
        StateMachine.TransitionTo(StateMachine.ChaseState);
    }

    protected void EnterFlea(GameObject bomb)
    {
        StateMachine.FleaState.Bomb = bomb;
        StateMachine.TransitionTo(StateMachine.FleaState);
    }

    protected void EnterReload()
    {
        StateMachine.TransitionTo(StateMachine.ReloadState);
    }

    protected void EnterKamikaze(GameObject bomb)
    {
        StateMachine.KamikazeState.Bomb = bomb;
        StateMachine.TransitionTo(StateMachine.KamikazeState);
    }
}
