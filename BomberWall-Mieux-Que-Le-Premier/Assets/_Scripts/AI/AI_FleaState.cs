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
        //chope un point derrière lui quand il regarde la bombe
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
