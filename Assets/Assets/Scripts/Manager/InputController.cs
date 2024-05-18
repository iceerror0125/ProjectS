using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InputController : SingletonMono<InputController>
{
    [SerializeField] private GameObject controlGroup;
    [SerializeField] private Joystick joyStick;
    [SerializeField] private CinemachineVirtualCamera cam;
    private void Start()
    {
        controlGroup.SetActive(true);
        joyStick.gameObject.SetActive(false);
    }

    private EventType eventType = EventType.None;

    private void ChangeControlMode()
    {
        controlGroup.SetActive(!controlGroup.activeSelf);
        joyStick.gameObject.SetActive(!joyStick.gameObject.activeSelf);
    }
    public void SetInteractEvent(EventType type)
    {
        eventType = type;
    }
    public void OnJumpButtonTap()
    {
        GameManager.instance.Player.Jump();
    }
    public void OnMoveLeftTap()
    {
        GameManager.instance.Player.MoveLeft();
    }
    public void OnMoveRightTap()
    {
        GameManager.instance.Player.MoveRight();
    }
    public void OnInteractWithObjectTap()
    {
        Observer.instance.Annouce(eventType);
    }

    private float maxX;
    private float maxY;
    public void OnBinocularTap()
    {
        // Observer.instance.Annouce(EventType.UseBinoculars);
        ChangeControlMode();
        if (!joyStick.gameObject.activeSelf)
        {
            cam.Follow = GameManager.instance.Player.transform;
            GameManager.instance.Player.ResetFakePlayerPosition();
            
        }
    }
    private void Update()
    {
        if (joyStick.gameObject.activeSelf)
        {

            if (joyStick.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x == 0)
            {
                cam.Follow = GameManager.instance.Player.transform;
                GameManager.instance.Player.ResetFakePlayerPosition();
            }
            else
            {
                maxX = GameManager.instance.Player.transform.localPosition.x + 5;
                maxY = GameManager.instance.Player.transform.localPosition.y + 5;
                Transform fakePlayer = GameManager.instance.Player.fakePlayer.transform;
                cam.Follow = fakePlayer;
                var horizontalMove = joyStick.Horizontal * 5;
                var verticalMove = joyStick.Vertical * 5;
                fakePlayer.transform.position = new Vector2(Mathf.Clamp(fakePlayer.position.x + horizontalMove * Time.deltaTime, -maxX, maxX ),
                    Mathf.Clamp(fakePlayer.position.y + verticalMove * Time.deltaTime, -maxY, maxY));
            }
        }
    }
}
