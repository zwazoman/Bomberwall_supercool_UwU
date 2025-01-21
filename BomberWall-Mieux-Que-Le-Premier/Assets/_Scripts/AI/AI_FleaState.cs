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
        Vector3 playerToBomb = Bomb.transform.position - StateMachine.transform.position;
        Vector3 fleaSpot = StateMachine.transform.position - playerToBomb;

        StateMachine.Controller.MoveTo(fleaSpot);

    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
    }

}
