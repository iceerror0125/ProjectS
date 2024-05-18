using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerState
{
    void Enter();
    void Exit();
    void Update();
}

public class PlayerState : IPlayerState
{
    protected Player player;
    public virtual void Enter()
    {
        player = GameManager.instance.Player;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {
        
    }
}

