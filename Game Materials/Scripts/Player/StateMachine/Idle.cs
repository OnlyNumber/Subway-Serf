using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public override void OnUpdate()
    {
        
        if(SpawnManager.instance.ISGAME)
        {
            StateManager.instance.GoToNextState(new Running());
        }


    }


}
