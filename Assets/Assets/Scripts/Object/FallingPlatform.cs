using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : TriggerObject
{
    [SerializeField] private bool canRespawn;
    [SerializeField] private float timeToRespawn;
    [SerializeField] private float timeToFall = 0.5f;
    [SerializeField] private BoxCollider2D col;
    private Rigidbody2D rb;
    private Vector2 originPosition;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        originPosition = transform.position;
    }
    public override void OnActiveCollisionEnter()
    {
        Invoke(nameof(FallController), timeToFall);
    }

    public override void OnActiveCollisionExit()
    {
    }

    private void FallController()
    {
        Fall();
        if (canRespawn)
        {
            Invoke(nameof(ReSpawn), timeToRespawn);
        }
    }
    private void Fall()
    {
        rb.gravityScale = GameConstants.fallGravity;
        col.isTrigger = true;
        anim.Play("Off");
    }
    private void ReSpawn()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        col.isTrigger = false;
        anim.Play("On");
        transform.position = originPosition;
    }

}
