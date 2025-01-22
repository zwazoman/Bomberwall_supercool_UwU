using UnityEngine;

public class AI_FleaState : AI_BaseState
{
    public GameObject Bomb;

    float _timer;

    Vector3 fleaSpot;

    public override void OnEnter()
    {
        StateMachine.Sensor.OnBombFar += EnterReload;
        //StateMachine.Sensor.OnBombVeryNear += EnterKamikaze;
    }

    void Flea()
    {
        StateMachine.Controller.MoveTo(fleaSpot);

    }

    public override void OnExit()
    {
        StateMachine.Sensor.OnBombFar -= EnterReload;
        //StateMachine.Sensor.OnBombVeryNear -= EnterKamikaze;


    }

    public override void Update()
    {
        Vector3 playerToBomb = Bomb.transform.position - StateMachine.transform.position;
        fleaSpot = StateMachine.transform.position - playerToBomb;

        _timer += Time.deltaTime;
        if (_timer > 0.2f)
        {
            Flea();
            _timer = 0;
        }
    }

}
