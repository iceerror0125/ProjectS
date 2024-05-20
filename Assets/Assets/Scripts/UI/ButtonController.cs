using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public LongClickButton left;
    public LongClickButton right;

    void Update()
    {
        if (left.IsPressed)
        {
            right.CloseButton();
            left.DoTapEvent();
        }
        if (right.IsPressed)
        {
            left.CloseButton();
            right.DoTapEvent();
        }
    }
    private void OnDisable()
    {
        right.DoUnTapEvent();
        left.DoUnTapEvent();
    }
}
