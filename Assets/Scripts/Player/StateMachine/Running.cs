using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : State
{
    public override void OnEnter()
    {
        base.OnEnter();

        StateManager.instance.animController.SetBool("IsRunning", true );

    }


    public override void OnUpdate()
    {
        base.OnUpdate();

        if(StateManager.instance.playerControl.UpCollider.enabled == false)
        {
            StateManager.instance.GoToNextState(new Slide());
        }

        if(StateManager.instance.playerControl.CheckGround() == false)
        {
            StateManager.instance.GoToNextState(new Jumping());
        }    

    }

    public override void OnExit()
    {
        base.OnExit();
        StateManager.instance.animController.SetBool("IsRunning", false);


    }

}
