using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{
    public override void OnEnter()
    {
        StateManager.instance.animController.SetTrigger("isDeath");
    }
}
