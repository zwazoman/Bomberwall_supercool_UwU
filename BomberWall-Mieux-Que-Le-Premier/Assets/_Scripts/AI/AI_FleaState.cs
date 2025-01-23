using Codice.CM.Client.Differences.Merge;
using UnityEngine;

public class AI_FleaState : AI_BaseState
{
    public GameObject Bomb;

    float _timer;

    Vector3 playerToBomb;

    public override void OnEnter()
    {
        //StateMachine.Sensor.OnBombVeryNear += EnterKamikaze;

        playerToBomb = Bomb.transform.position - StateMachine.transform.position;
        Vector3 fleaSpot = StateMachine.transform.position - playerToBomb;

        FleaTo(StateMachine.Sensor.GetClosestNavmeshPoint(fleaSpot.normalized * 2));
    }

    void FleaTo(Vector3 destination)
    {
        Debug.Log("flea to" + destination);
        StateMachine.Controller.MoveTo(destination);
    }

    void StopFleaing()
    {
        EnterReload();
    }

    public override void OnExit()
    {
        //StateMachine.Sensor.OnBombVeryNear -= EnterKamikaze;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 0.2f)
        {
            if(Bomb == null) StopFleaing();
            else 
            {
                playerToBomb = Bomb.transform.position - StateMachine.transform.position;

                if (playerToBomb.magnitude >= StateMachine.Sensor.BombDetectionrange) StopFleaing();
            }
            _timer = 0;
        }
    }

}
