using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectArrowAnimation : MonoBehaviour
{
    void Start()
    {
        SpinAround(180f);
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    private void SpinAround(float target)
    {
        transform.DORotate(new Vector3(0, target, 0), 1).OnComplete(() =>
        {
            target = 180 - Mathf.Abs(transform.eulerAngles.y);
            SpinAround(target);
        });
    }
}
