using UnityEngine;

public class DefenseMode : BasePlayerMode
{
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        player.Speed = player.Speed * 0.8f;
        player.JumpForce = player.JumpForce * 0.8f;
        Debug.Log("Defense Mode Ativado");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        player.Speed = player._baseSpeed;
        player.JumpForce = player._baseJumpForce;
        Debug.Log("Defense Mode Desativado");
    }

     public override void HandleAction1()
    {
        
    }

     public override void HandleAction2()
    {

    }
}