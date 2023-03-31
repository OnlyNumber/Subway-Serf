using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StateManager.instance.GoToNextState(new Dead());
            SpawnManager.instance.ISGAME = false;
            
        }
    }
}
