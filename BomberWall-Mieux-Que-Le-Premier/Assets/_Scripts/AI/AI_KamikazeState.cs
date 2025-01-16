using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_KamikazeState : AI_BaseState
{
    public override void OnEnter()
    {
        StateMachine.Sensor.OnBombFar += EnterReflexion;
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        
    }

}
