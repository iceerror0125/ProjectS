using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        // player.stateMachine.ChangeState(EPlayerAction.Idle);
    }

    public override void Update()
    {
        base.Update();
       
    }
}
