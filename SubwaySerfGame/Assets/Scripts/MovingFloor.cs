using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    private Rigidbody myRb;

    
    private float speed;

    [SerializeField]
    private float currentSpeed;

    private Vector3 firstPos = new Vector3(0, 0, 185.5f);

    private Vector3 finishPos = new Vector3(0, 0, -85.9f);

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();

        SpawnManager.instance.onIsGameFinish += GameIsOff;
        SpawnManager.instance.onIsGameStart += GameIsOn;
    }

    void Update()
    {
        myRb.velocity = new Vector3(0, 0, -speed);

        if (transform.position.z <= finishPos.z)
        {


            transform.position = firstPos;
        }

    }

    private void GameIsOff()
    {
        speed = 0;
    }

    private void GameIsOn()
    {
        speed = currentSpeed;
    }

}
