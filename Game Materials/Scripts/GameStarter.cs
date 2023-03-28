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

            SpawnManager.instance.ISGAME = true;
        }
    }

/*private void OnMouseDown()
    {
        Debug.Log("Work");
    }*/

}
