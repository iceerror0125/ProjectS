using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private bool canInteractable = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!canInteractable)
                return;

            OnPlayerEnter();
        }
        else
        {
            OnObjectEnter(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!canInteractable)
                return;

            OnPlayerExit();
        }
        else
        {
            OnObjectExit(collision);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!canInteractable)
                return;

            OnPlayerStay();
        }
        else
        {
            OnObjectStay(collision);
        }
    }
    protected virtual void Interacted()
    {
        canInteractable = false;
    }
    protected virtual void Notify() { Interacted(); }

    protected virtual void OnObjectEnter(Collider2D collision) { }
    protected virtual void OnObjectExit(Collider2D collision) { }
    protected virtual void OnObjectStay(Collider2D collision) { }
    protected virtual void OnPlayerEnter() { }
    protected virtual void OnPlayerExit() { }
    protected virtual void OnPlayerStay() { }
}
