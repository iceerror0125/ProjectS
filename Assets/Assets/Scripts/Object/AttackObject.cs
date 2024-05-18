using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AttackObject : InteractableObject
{
    [SerializeField] private GameObject active;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }
    protected override void OnObjectEnter(Collider2D collision)
    {
        base.OnObjectEnter(collision);

        if (Mathf.Abs(rb.velocity.x) > 0.1f && collision.gameObject.layer == 8)
        {
            rb.velocity = Vector3.zero;
        }
    }
    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        Observer.instance.Subscribe(EventType.AttackBox, Notify);
        InputController.instance.SetInteractEvent(EventType.AttackBox);
        active.SetActive(true);
    }
    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();

        Observer.instance.UnSubscribe(EventType.AttackBox, Notify);
        InputController.instance.SetInteractEvent(EventType.None);
        active.SetActive(false);
    }
    protected override void Notify()
    {
        base.Notify();

        Player player = GameManager.instance.Player;
        player.Interact();

        if (player.transform.position.x > transform.position.x && player.dir > 0)
        {
            player.ChangeDir(-1);
        }
        else if (player.transform.position.x < transform.position.x && player.dir < 0)
        {
            player.ChangeDir(1);
        }

        float atkDir = player.transform.position.x > transform.position.x ? -1 : 1;
        rb.AddForce(new Vector2(atkDir * 20, rb.velocity.y), ForceMode2D.Impulse);
        col.offset = Vector2.zero;
        col.size = new Vector2(1, 1);
    }
    protected override void Interacted()
    {
        base.Interacted();
        active.SetActive(false);
    }
}
