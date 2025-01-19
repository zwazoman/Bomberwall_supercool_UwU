using UnityEngine;

public class AI_StateMachine : MonoBehaviour
{
    public Ai_Sensor Sensor;
    public AI_Controller Controller;

    public AI_BaseState CurrentState { get; private set; }

    public AI_ReflexionState ChaseState;
    public AI_FleaState FleaState;
    public AI_ReloadState ReloadState;
    public AI_KamikazeState KamikazeState;

    private void Start()
    {
        ChaseState = new AI_ReflexionState();
        ChaseState.StateMachine = this;

        FleaState = new AI_FleaState();
        FleaState.StateMachine = this;


        ReloadState = new AI_ReloadState();
        ReloadState.StateMachine = this;

        KamikazeState = new AI_KamikazeState();
        KamikazeState.StateMachine = this;

        CurrentState = ReloadState;
    }

    public void TransitionTo(AI_BaseState state)
    {
        if (state == CurrentState) return;
        CurrentState.OnExit();

        CurrentState = state;
        CurrentState.OnEnter();
    }

    private void Update()
    {
        CurrentState.Update();
    }
}
