using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    private Inventory inventory = new Inventory();
    [SerializeField] private Player player;

    public Inventory Inventory => inventory;
    public Player Player => player;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
