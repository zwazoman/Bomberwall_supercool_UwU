using UnityEngine;

public class AI_KamikazeState : AI_BaseState
{
    public GameObject Bomb;

    public override void OnEnter()
    {
        StateMachine.Sensor.OnBombNear += EnterFlea;
        StateMachine.Sensor.OnBombFar += EnterReload;

        //StateMachine.Controller.OnTargetReached += HitBomb;

        HitBomb();
    }

    void CatchBomb()
    {
        StateMachine.Controller.MoveTo(Bomb.transform.position);
    }

    void HitBomb()
    {
        StateMachine.Controller.LookTo(Bomb.transform.position);
        StateMachine.Controller.Hit();
    }

    public override void OnExit()
    {
        StateMachine.Sensor.OnBombNear -= EnterFlea;
        StateMachine.Sensor.OnBombFar -= EnterChase;

        //StateMachine.Controller.OnTargetReached += HitBomb;
    }

    public override void Update()
    {
        
    }

}
