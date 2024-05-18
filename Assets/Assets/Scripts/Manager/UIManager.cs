using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMono<UIManager>
{
    public GameObject keyUI;

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
    }
    public void GetKey()
    {
        keyUI.SetActive(true);
        GameManager.instance.Inventory.AddKey();
    }
    public void UseKey()
    {
        keyUI.SetActive(false);
        GameManager.instance.Inventory.RemoveKey();
    }
}
