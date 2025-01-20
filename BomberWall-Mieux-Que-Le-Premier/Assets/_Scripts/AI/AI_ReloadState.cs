using System;
using UnityEngine;

public class AI_ReloadState : AI_BaseState
{
    GameObject _bomb;

    public override void OnEnter()
    {
        StateMachine.Controller.Bomb.OnBombPickUp += PickedUpBomb;

        Reload();
    }

    void Reload()
    {
        if(God.Instance._bombPickups.Count == 0 && StateMachine.Controller.Bomb.BombsPossessedCount > 0)
        {
            EnterChase();
        }

        _bomb = StateMachine.Sensor.GetClosestPickup().gameObject;
        StateMachine.Controller.MoveTo(_bomb.transform.position);
    }

    void PickedUpBomb()
    {
        float factor = 0.5f + StateMachine.Controller.Bomb.BombsPossessedCount / 10;
        if(UnityEngine.Random.value < factor || StateMachine.Controller.Bomb.BombsPossessedCount == God.Instance.StartPickupCount)
        {
            EnterChase() ;
        }
    }

    public override void OnExit()
    {
        StateMachine.Controller.Bomb.OnBombPickUp -= PickedUpBomb;

        _bomb = null;
    }

    public override void Update()
    {
        if(_bomb ==null) Reload();
    }
}
