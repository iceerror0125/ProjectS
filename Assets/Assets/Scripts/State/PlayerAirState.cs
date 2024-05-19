using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.IsGroundValue(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(EPlayerAction.Fall);
        }
    }
}
