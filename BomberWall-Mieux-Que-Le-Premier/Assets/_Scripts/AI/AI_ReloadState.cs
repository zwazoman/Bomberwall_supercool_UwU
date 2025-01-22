using Codice.CM.Client.Differences.Merge;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class AI_ReloadState : AI_BaseState
{
    GameObject _closestPickup;

    float _timer;


    public override void OnEnter()
    {
        StateMachine.Controller.Bomb.OnBombPickUp += PickedUpBomb;
        StateMachine.Sensor.OnBombNear += EnterFlea;

        Roll();
    }

    void Roll()
    {
        float factor = 0.25f + StateMachine.Controller.Bomb.BombsPossessedCount / 10;
        if ((UnityEngine.Random.value < factor || God.Instance.BombPickups.Count == 0) && StateMachine.Controller.Bomb.BombsPossessedCount > 0)
        {
            EnterChase();
        }
    }

    void PickedUpBomb()
    {
        Roll();
    }

    public override void OnExit()
    {
        StateMachine.Controller.Bomb.OnBombPickUp -= PickedUpBomb;
        StateMachine.Sensor.OnBombNear -= EnterFlea;

        _closestPickup = null;
    }

    public override void Update()
    {
        if (StateMachine.Sensor.PlayerNear && StateMachine.Controller.Bomb.BombsPossessedCount > 0) EnterChase();

        _timer += Time.deltaTime;
        if (_timer > 0.2f)
        {
            _closestPickup = StateMachine.Sensor.GetClosestPickup().gameObject;
            StateMachine.Controller.MoveTo(_closestPickup.transform.position);
        }

        //if (_closestPickup == null)
        //{
        //    Debug.Log("Bomb Taken");
        //    Roll();
        //}
    }
}
