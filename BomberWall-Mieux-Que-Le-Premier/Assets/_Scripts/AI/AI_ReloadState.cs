using Codice.CM.Client.Differences.Merge;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class AI_ReloadState : AI_BaseState
{
    GameObject _closestPickup;

    public override void OnEnter()
    {
        StateMachine.Controller.Bomb.OnBombPickUp += PickedUpBomb;

        Reload();
    }

    async void Reload()
    {
        await Task.Delay(1);
        if(God.Instance.BombPickups.Count == 0 && StateMachine.Controller.Bomb.BombsPossessedCount > 0)
        {
            EnterChase();
        }

        _closestPickup = StateMachine.Sensor.GetClosestPickup().gameObject;
        StateMachine.Controller.MoveTo(_closestPickup.transform.position);
    }

    void PickedUpBomb()
    {
        Debug.Log("singeos");
        float factor = 0.5f + StateMachine.Controller.Bomb.BombsPossessedCount / 10;
        if(UnityEngine.Random.value < factor || StateMachine.Controller.Bomb.BombsPossessedCount == God.Instance.StartPickupCount)
        {
            Debug.Log("Chase");
            EnterChase();
        }
        else
        {
            Reload();
        }
    }

    public override void OnExit()
    {
        StateMachine.Controller.Bomb.OnBombPickUp -= PickedUpBomb;
        StateMachine.Controller.OnTargetReached -= PickedUpBomb;

        _closestPickup = null;
    }

    public override void Update()
    {
        //if (_closestPickup == null)
        //{
        //    Debug.Log("Bomb Taken");
        //    Reload();
        //}
    }
}
