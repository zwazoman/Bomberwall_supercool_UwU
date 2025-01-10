using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI_StateMachine : MonoBehaviour
{
    public Ai_Sensor Sensor;
    public AI_Controller Controller;

    AI_BaseState currentState;

    AI_ChaseState chaseState;
    AI_FleaState fleaState;
    AI_ReloadState reloadState;

    private void Start()
    {
        chaseState = new AI_ChaseState();
        chaseState.StateMachine = this;

        fleaState = new AI_FleaState();
        fleaState.StateMachine = this;


        reloadState = new AI_ReloadState();
        reloadState.StateMachine = this;

        currentState = reloadState;
    }

    public void TransitionTo(AI_BaseState state)
    {
        currentState.OnExit();

        currentState = state;
        currentState.OnEnter();
    }

    private void Update()
    {
        currentState.Update();
    }
}
