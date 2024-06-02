using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InputController : SingletonMono<InputController>
{
    [SerializeField] private GameObject controlGroup;
    [SerializeField] private Joystick joyStick;
    [SerializeField] private CinemachineVirtualCamera cam;

    private Player player;
    private void Start()
    {
        player = GameManager.instance.Player;
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

    private float maxDistanceX = 5;
    private float maxDistanceY = 5;
    private float cameraSpeed = 10;
    public void OnBinocularTap()
    {
        // Observer.instance.Annouce(EventType.UseBinoculars);
        ChangeControlMode();
        if (!joyStick.gameObject.activeSelf)
        {
            cam.Follow = player.transform;
            player.ResetFakePlayerPosition();
            player.ToIdle();
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
                Transform fakePlayer = GameManager.instance.Player.fakePlayer.transform;
                cam.Follow = fakePlayer;

                var horizontalMoveSpeed = joyStick.Horizontal * cameraSpeed;
                var verticalMoveSpeed = joyStick.Vertical * cameraSpeed;
                fakePlayer.transform.localPosition = new Vector2(
                    Mathf.Clamp(fakePlayer.localPosition.x + horizontalMoveSpeed * Time.deltaTime * GameManager.instance.Player.dir, -maxDistanceX, maxDistanceX),
                    Mathf.Clamp(fakePlayer.localPosition.y + verticalMoveSpeed * Time.deltaTime, -maxDistanceY, maxDistanceY)
                    );
            }
        }
#if UNITY_EDITOR
        PlayOnPC();
#endif
    }

    private void PlayOnPC()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnBinocularTap();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnMoveLeftTap();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            OnMoveRightTap();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnJumpButtonTap();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnInteractWithObjectTap();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (player.stateMachine.currentState is PlayerGroundState)
            {
                player.ToIdle();
            }
        }
    }
}
