using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    [SerializeField] private AnimationEventHandler animHandler;

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private Transform groundCheckForward;
    [SerializeField] private Transform groundCheckBack;
    public GameObject fakePlayer; // use to move camera in observe mode
    private float groundCheckLenght = 0.1f;

    public StateMachine stateMachine { get; private set; }
    public float dir { get; private set; }
    public bool isGround { get; private set; }
    public void IsGroundValue(bool value) => isGround = value;

    private void OnEnable()
    {
        animHandler.OnFinishTrigger += Idle;
    }
    private void OnDisable()
    {
        animHandler.OnFinishTrigger -= Idle;
    }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public bool CheckIsGround()
    {
        var raycastForward = Physics2D.Raycast(groundCheckForward.position, Vector2.down, groundCheckLenght);
        var raycastBack = Physics2D.Raycast(groundCheckBack.position, Vector2.down, groundCheckLenght);
        Debug.Log($"{raycastForward.collider is null}");
        /*  if (raycastForward.collider != null && raycastBack.collider != null && CheckIsGroundLayer(raycastBack.collider.gameObject.layer) && CheckIsGroundLayer(raycastForward.collider.gameObject.layer))
          {
              return true;
          }
          return false;*/
        if (raycastForward.collider is null && raycastBack.collider is null)
        {
            return false;
        }
        return true;
    }

    private bool CheckIsGroundLayer(int layer)
    {
        switch (layer)
        {
            case (int)EGameLayerName.Ground: return true;
            case (int)EGameLayerName.TriggerObject: return true;
            default: return false;
        }
    }
    private void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        dir = 1;

        stateMachine = new StateMachine(this);
        stateMachine.InitState(EPlayerAction.Idle);
    }
    public void MoveLeft()
    {
        rb.velocity = new Vector2(moveSpeed * -1, rb.velocity.y);
        ChangeDir(-1);
    }
    public void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        ChangeDir(1);
    }
    public void Jump()
    {
        if (stateMachine.currentState is PlayerGroundState)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stateMachine.ChangeState(EPlayerAction.Jump);
        }
    }
    public void AddForce(float x = 0, float y = 0)
    {
        rb.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }
    public void ToIdle()
    {
        if (stateMachine.currentState is PlayerGroundState)
            Idle();
    }
    private void Idle()
    {
        stateMachine.ChangeState(EPlayerAction.Idle);
    }
    public void Interact()
    {
        stateMachine.ChangeState(EPlayerAction.Interact);
    }
    public void ResetFakePlayerPosition()
    {
        fakePlayer.transform.position = this.transform.position;
    }
    public void ChangeDir(float dir)
    {
        this.dir = dir;
        transform.localScale = new Vector2(Mathf.Clamp(dir, -1, 1), transform.localScale.y);
    }
    public void ZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    public void ChangeToFallGravity() { rb.gravityScale = GameConstants.fallGravity; }
    public void ChangeToDefaultGravity() { rb.gravityScale = GameConstants.defaultGravity; }
}
