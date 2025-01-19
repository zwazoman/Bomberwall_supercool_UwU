using UnityEngine.TextCore.Text;

public class AI_ChaseState : AI_BaseState
{
    public override void OnEnter()
    {
        StateMachine.Sensor.OnPlayerInRange += Attack;
    }

    void Chase()
    {
        // fonce vers le joueur
    }

    void Attack()
    {
        // se tourne vers le joueur et balance un nombre random de bombes parmis celles qu'il possède (il doit attendre entre les lancers pour les anims)
        EnterReload();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
