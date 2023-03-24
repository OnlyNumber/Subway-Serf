using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("GameStarter");

            SpawnManager.instance.ISGAME = true;
        }
    }

}
