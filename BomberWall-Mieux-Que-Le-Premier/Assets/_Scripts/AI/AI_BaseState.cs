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

    protected void EnterFlea()
    {
        StateMachine.TransitionTo(StateMachine.FleaState);
    }

    protected void EnterReload()
    {
        StateMachine.TransitionTo(StateMachine.ReloadState);
    }

    protected void EnterKamikaze()
    {
        StateMachine.TransitionTo(StateMachine.KamikazeState);
    }

    protected void EnterReflexion()
    {
        StateMachine.TransitionTo(StateMachine.ReflexionState);
    }
}
