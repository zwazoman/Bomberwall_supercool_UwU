using Codice.CM.Client.Differences.Merge;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AI_ChaseState : AI_BaseState
{
    public GameObject Player;

    float _timer = 0;

    bool _isAttacking = false;

    public override void OnEnter()
    {
        StateMachine.Sensor.OnPlayerNear += Attack;
        StateMachine.Sensor.OnBombNear += EnterFlea;

        Chase();
    }

    void Chase()
    {
        Player = StateMachine.Sensor.GetClosestPlayer();
        StateMachine.Controller.MoveTo(Player.transform.position);
    }

    async void Attack()
    {
        if (_isAttacking) return;

        Debug.Log("Attack");
        _isAttacking = true;
        StateMachine.Controller.StopMoving();

        int BombsToThrow = Random.Range(1,StateMachine.Controller.Bomb.BombsPossessedCount);
        for(int i = 0; i < BombsToThrow; i++)
        {
            Debug.Log("ThrowBomb");
            StateMachine.Controller.LookTo(Player.transform.position);
            StateMachine.Controller.Bomb.Equip();
            await Task.Delay(500);
            StateMachine.Controller.Bomb.Throw();
            await Task.Delay(500);
        }

        StateMachine.Controller.RestartMoving();
        _isAttacking = false;

        EnterReload();
    }

    public override void OnExit()
    {
        StateMachine.Sensor.OnPlayerNear -= Attack;
        StateMachine.Sensor.OnBombNear -= EnterFlea;
    }

    public override void Update()
    {
        if (_isAttacking) return;

        _timer += Time.deltaTime;
        if(_timer > 0.5f)
        {
            Chase();
            _timer = 0;
        }
    }
}
