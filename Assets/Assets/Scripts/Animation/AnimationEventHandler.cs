using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinishTrigger;

    public void AnimationFinishTrigger() => OnFinishTrigger?.Invoke();
}
