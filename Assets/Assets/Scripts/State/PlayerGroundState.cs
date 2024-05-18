using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(player.rb.velocity.x) > 0.2f)
        {
            player.stateMachine.ChangeState(EPlayerAction.Run);
        }
    }
}
