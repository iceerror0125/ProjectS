using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineTrigger : MonoBehaviour
{
    [SerializeField] private float pushForce = 5;
    public void PushPlayer()
    {
        GameManager.instance.Player.AddForce(0, pushForce);
    }
}
