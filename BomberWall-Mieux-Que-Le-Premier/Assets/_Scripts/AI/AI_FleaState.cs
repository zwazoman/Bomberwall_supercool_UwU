using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_FleaState : AI_BaseState
{
    public GameObject Bomb;

    public override void OnEnter()
    {
        StateMachine.Sensor.OnBombFar += EnterChase;

        Flea();
    }

    void Flea()
    {

    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

}
