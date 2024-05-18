using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int key;

    public int Key => key;
    public void AddKey()
    {
        key++;
    }
    public void RemoveKey() { key--; }
}
