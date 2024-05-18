using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    // public LongClickButton otherButton;
    public Player player;
    public UnityEvent onLongClick;
    public UnityEvent onUnClick;

    private bool isPressed;
    public bool IsPressed => isPressed;
   

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        DoUnTapEvent();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DoUnTapEvent();
    }
    public void CloseButton()
    {
        isPressed = false;
    }
    public void DoTapEvent()
    {
        onLongClick.Invoke();
    }
    public void DoUnTapEvent()
    {
        isPressed = false;
        onUnClick.Invoke();
    }

   
}
