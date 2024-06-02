using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : TriggerObject
{
    private string activeName = "TrampolineActive";
   
    public override void OnActiveCollisionEnter()
    {
        float distance = player.transform.position.y - transform.position.y;
        if (distance > 0.2f)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(activeName))
            {
                anim.Play(activeName);
            }
        }
    }

    public override void OnActiveCollisionExit()
    {
        
    }
}
