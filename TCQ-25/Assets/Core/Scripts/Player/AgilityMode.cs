using UnityEngine;

public class AgilityMode : BasePlayerMode
{
    public override void EnterMode(PlayerController player)
    {
        base.EnterMode(player);
        player.Speed = player.Speed * 1.3f;
        player.JumpForce = player.JumpForce * 1.5f;
        Debug.Log("Agility Mode Ativado");
    }
    
    public override void ExitMode()
    {
        base.ExitMode();
        player.Speed = player._baseSpeed;
        player.JumpForce = player._baseJumpForce;
        Debug.Log("Agility Mode Desativado");
    }

     public override void HandleAction1()
    {
        
    }

     public override void HandleAction2()
    {

    }
}