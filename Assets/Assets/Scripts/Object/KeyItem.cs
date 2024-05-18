using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : InteractableObject
{
    
    private BoxCollider2D col;
    private void OnEnable()
    {
        Observer.instance.Subscribe(EventType.GetKey, Notify);
    }
    private void OnDisable()
    {
        Observer.instance.UnSubscribe(EventType.GetKey, Notify);
    }
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }
    protected override void Notify()
    {
        base.Notify();
        UIManager.instance.GetKey();
    }

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        Observer.instance.Annouce(EventType.GetKey);
        gameObject.SetActive(false);
    }
}
