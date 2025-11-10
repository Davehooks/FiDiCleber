using UnityEngine;

public class AttackMode : BasePlayerMode
{
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        Debug.Log("Attack Mode Ativado");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        Debug.Log("Attack Mode Desativado");
    }

     public override void HandleAction1()
    {
        
    }

     public override void HandleAction2()
    {

    }
}