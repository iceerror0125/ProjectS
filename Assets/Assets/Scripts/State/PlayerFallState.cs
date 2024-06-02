using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public override void Enter()
    {
        base.Enter();
        player.ChangeToFallGravity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.CheckIsGround())
        {
            player.stateMachine.ChangeState(EPlayerAction.Idle);
        }
    }
}
