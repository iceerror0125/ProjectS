using System.Collections;
using UnityEngine;

public class Gate : InteractableObject
{
    [SerializeField] private GameObject active;

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
        Observer.instance.Subscribe(EventType.UseKey, Notify);
        InputController.instance.SetInteractEvent(EventType.UseKey);
        active.SetActive(true);
    }
    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();
        Observer.instance.UnSubscribe(EventType.UseKey, Notify);
        InputController.instance.SetInteractEvent(EventType.None);
        active.SetActive(false);
    }

    protected override void Notify()
    {
        if (GameManager.instance.Inventory.Key > 0)
        {
            base.Notify();
            StartCoroutine(MoveRoutine());
        }
    }

    protected override void Interacted()
    {
        base.Interacted();
        active.SetActive(false);
    }

    private IEnumerator MoveRoutine()
    {
        UIManager.instance.UseKey();
        float counter = 0;
        Vector3 target = transform.position + Vector3.up * 3;
        while (counter < 1)
        {
            counter += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, 5 * Time.deltaTime);
            yield return null;
        }

    }
}
