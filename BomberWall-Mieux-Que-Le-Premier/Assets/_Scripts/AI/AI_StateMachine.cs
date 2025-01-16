using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI_StateMachine : MonoBehaviour
{
    public Ai_Sensor Sensor;
    public AI_Controller Controller;

    AI_BaseState currentState;

    public AI_ReflexionState ChaseState;
    public AI_FleaState FleaState;
    public AI_ReloadState ReloadState;
    public AI_KamikazeState KamikazeState;
    public AI_ReflexionState ReflexionState;

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

        ReflexionState = new AI_ReflexionState();
        ReflexionState.StateMachine = this;

        currentState = ReloadState;
    }

    public void TransitionTo(AI_BaseState state)
    {
        if (state == currentState) return;
        currentState.OnExit();

        currentState = state;
        currentState.OnEnter();
    }

    private void Update()
    {
        currentState.Update();
    }
}
