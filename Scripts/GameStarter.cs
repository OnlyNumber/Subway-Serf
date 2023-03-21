using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spawnManager.ISGAME = true;
            gameObject.SetActive(false);
        }
    }

}
