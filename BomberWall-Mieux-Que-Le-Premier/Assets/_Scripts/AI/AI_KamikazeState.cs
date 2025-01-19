using UnityEngine;

public class AI_KamikazeState : AI_BaseState
{
    public GameObject Bomb;

    public override void OnEnter()
    {
        StateMachine.Sensor.OnBombNear += EnterFlea;
        StateMachine.Sensor.OnBombFar += EnterChase;

        StateMachine.Controller.OnTargetReached += HitBomb;

        CatchBomb();
    }

    void CatchBomb()
    {
        StateMachine.Controller.MoveTo(Bomb.transform.position);
    }

    void HitBomb()
    {
        StateMachine.Controller.Hit();
    }

    public override void OnExit()
    {
        StateMachine.Sensor.OnBombNear -= EnterFlea;
    }

    public override void Update()
    {
        
    }

}
