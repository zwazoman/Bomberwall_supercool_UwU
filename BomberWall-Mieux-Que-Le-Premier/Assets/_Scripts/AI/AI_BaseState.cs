using UnityEngine;

public abstract class AI_BaseState
{
    public AI_StateMachine StateMachine;

    public abstract void OnEnter();
    public abstract void Update();
    public abstract void OnExit();

    protected void EnterChase()
    {
        Debug.Log("Chase");
        StateMachine.TransitionTo(StateMachine.ChaseState);
    }

    protected void EnterFlea(GameObject bomb)
    {
        Debug.Log("Flea");
        StateMachine.FleaState.Bomb = bomb;
        StateMachine.TransitionTo(StateMachine.FleaState);
    }

    protected void EnterReload()
    {
        Debug.Log("Reload");
        StateMachine.TransitionTo(StateMachine.ReloadState);
    }

    protected void EnterKamikaze(GameObject bomb)
    {
        Debug.Log("Kamikaze");
        StateMachine.KamikazeState.Bomb = bomb;
        StateMachine.TransitionTo(StateMachine.KamikazeState);
    }
}
