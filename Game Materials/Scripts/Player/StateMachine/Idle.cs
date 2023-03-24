using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public override void OnEnter()
    {
        base.OnEnter();

        StateManager.instance.animController.SetTrigger("IdlePos");

    }

    public override void OnUpdate()
    {
        Debug.Log("IDLE NOW");
        
        if(SpawnManager.instance.ISGAME == true)
        {
            //Debug.Log("IDLE");

            StateManager.instance.GoToNextState(new Running());
        }


    }


}
