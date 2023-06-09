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

       // Debug.Log("Jumping");


        if (StateManager.instance.playerControl.upCollider.enabled == false)
        {
            StateManager.instance.GoToNextState(new Slide());
        }

        if (StateManager.instance.playerControl.CheckGround() == true)
        {
            StateManager.instance.GoToNextState(new Running());
        }


    }

    public override void OnExit()
    {
        base.OnExit();

        StateManager.instance.animController.SetBool("isJumping", false);

    }
}
