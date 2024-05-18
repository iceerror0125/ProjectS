using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerAction
{
    Idle,
    Jump,
    Fall,
    Run,
    Interact,
    Hurt
}
public class StateMachine
{
    public Player player { get; private set; }
    private Animator anim;
    private EPlayerAction currentAction;
    public IPlayerState currentState { get; private set; }

    private PlayerFallState fallState;
    private PlayerIdleState idleState;
    private PlayerInteractState interactState;
    private PlayerJumpState jumpState;
    private PlayerRunState runState;

    public StateMachine(Player player)
    {
        this.player = player;
        anim = player.anim;


        fallState = new PlayerFallState();
        idleState = new PlayerIdleState();
        interactState = new PlayerInteractState();
        jumpState = new PlayerJumpState();
        runState = new PlayerRunState();
    }
    public void InitState(EPlayerAction action)
    {
        SetCurrentState(action);
        currentState.Enter();
        currentAction = action;
    }
    public void ChangeState(EPlayerAction action)
    {
       /* if (action == currentAction)
            return;*/

        currentAction = action;
        currentState.Exit();
        SetCurrentState(action);
        currentState.Enter();
    }

    private void SetCurrentState(EPlayerAction action)
    {
        switch (action)
        {
            case EPlayerAction.Run: 
                currentState = runState;
                anim?.Play("Run");
                break;
            case EPlayerAction.Jump: 
                currentState = jumpState; 
                anim?.Play("Jump");
                break;
            case EPlayerAction.Fall: 
                currentState = fallState;
                anim?.Play("Fall");
                break;
            case EPlayerAction.Interact: 
                currentState = interactState; 
                anim?.Play("Interact");
                break;
            case EPlayerAction.Hurt:
                currentState = interactState;
                anim?.Play("Hurt");
                break;
            default:
                currentState = idleState;
                anim?.Play("Idle");
                break;
        }
    }
}
