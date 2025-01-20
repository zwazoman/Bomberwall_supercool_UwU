using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AI_ChaseState : AI_BaseState
{
    public GameObject Player;

    public override void OnEnter()
    {
        StateMachine.Sensor.OnPlayerInRange += Attack;
    }

    void Chase()
    {
        StateMachine.Controller.MoveTo(StateMachine.Sensor.GetClosestPlayer().transform.position);
    }

    async void Attack()
    {
        // se tourne vers le joueur et balance un nombre random de bombes parmis celles qu'il possède (il doit attendre entre les lancers pour les anims)
        int BombsToThrow = Random.Range(1,StateMachine.Controller.Bomb.BombsPossessedCount);
        for(int i = 0; i < BombsToThrow; i++)
        {
            StateMachine.Controller.LookTo(Player.transform.position);
            StateMachine.Controller.Bomb.Equip();
            await Task.Delay(500);
            StateMachine.Controller.Bomb.Throw();
        }
        EnterReload();
    }

    public override void OnExit()
    {
        StateMachine.Sensor.OnPlayerInRange -= Attack;
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
