using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    private SpawnManager spMan;

    void Start()
    {
        spMan = FindObjectOfType<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            spMan.ISGAME = false;
            
        }
    }
}
