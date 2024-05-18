using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public GameObject fakePlayer; // use to move camera in observe mode
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    public float dir {get; private set;}
    public bool isGround { get; private set; }

    #region State
    public StateMachine stateMachine { get; private set; }

    #endregion

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        stateMachine.currentState.Update();
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        stateMachine.ChangeState(EPlayerAction.Jump);
    }
    public void Idle()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            ZeroVelocity();
            stateMachine.ChangeState(EPlayerAction.Idle);

        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            stateMachine.ChangeState(EPlayerAction.Idle);
        }
    }
}
