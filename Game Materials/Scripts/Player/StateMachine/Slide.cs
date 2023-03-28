using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : State
{
    public override void OnEnter()
    {
        base.OnEnter();

        StateManager.instance.animController.SetBool("isSliding", true);

       // StateManager.instance.animController.SetTrigger("IsSlideTrigger");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Debug.Log("SLIDING");

        if (StateManager.instance.playerControl.CheckGround() == false)
        {
            StateManager.instance.GoToNextState(new Jumping());
        }

        if (StateManager.instance.playerControl.upCollider.enabled == true)
        {
            StateManager.instance.GoToNextState(new Running());
        }

        


    }

    public override void OnExit()
    {
        base.OnEnter();

        StateManager.instance.animController.SetBool("isSliding", false);

    }

}
