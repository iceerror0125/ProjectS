using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.ChangeToDefaultGravity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(player.rb.velocity.x) > 0.1f)
        {
            player.stateMachine.ChangeState(EPlayerAction.Run);
        }

        if (!player.CheckIsGround())
        {
            player.stateMachine.ChangeState(EPlayerAction.Fall);
        }
    }
}
