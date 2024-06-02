using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObject : MonoBehaviour
{
    protected Animator anim;
    protected Player player;
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameManager.instance.Player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)EGameLayerName.Player)
        {
            OnActiveCollisionEnter();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)EGameLayerName.Player)
        {
            OnActiveCollisionExit();
        }
    }

    public abstract void OnActiveCollisionEnter();
    public abstract void OnActiveCollisionExit();
}
