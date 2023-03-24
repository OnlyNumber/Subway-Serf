using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : State
{
    public override void OnEnter()
    {
        base.OnEnter();

        StateManager.instance.animController.SetTrigger("isRunning" );

    }


    public override void OnUpdate()
    {
        base.OnUpdate();

       // Debug.Log("Running");


        //StateManager.instance.animController.SetBool("isRunning", );

        if(StateManager.instance.playerControl.upCollider.enabled == false)
        {
            StateManager.instance.GoToNextState(new Slide());
        }

        if(StateManager.instance.playerControl.CheckGround() == false)
        {
            StateManager.instance.GoToNextState(new Jumping());
        }    

    }

    /*public override void OnExit()
    {
        base.OnExit();
        StateManager.instance.animController.SetBool("isRunning", false);


    }*/

}
