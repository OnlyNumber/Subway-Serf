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
        if(SpawnManager.instance.ISGAME == true)
        {
            StateManager.instance.GoToNextState(new Running());
        }


    }


}
