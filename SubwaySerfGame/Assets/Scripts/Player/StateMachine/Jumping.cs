using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State
{
    public override void OnEnter()
    {
        base.OnEnter();

        StateManager.instance.animController.SetBool("isJumping", true);

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (StateManager.instance.playerControl.CheckGround() == true)
        {

            if (StateManager.instance.playerControl.UpCollider.enabled == false)
            {
                StateManager.instance.GoToNextState(new Slide());
                return;
            }

            StateManager.instance.GoToNextState(new Running());
        }


    }

    public override void OnExit()
    {
        base.OnExit();

        StateManager.instance.animController.SetBool("isJumping", false);

    }
}
