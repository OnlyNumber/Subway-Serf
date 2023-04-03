using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    private Rigidbody myRb;

    [SerializeField]
    private float speed;

    public float Speed
    {
        set
        {
            if (value > 0)
            {
                speed = value;
            }
        }
        get
        {
            return speed;
        }


    }

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
        myRb.velocity = new Vector3(0, 0, -currentSpeed);

        if (transform.position.z <= finishPos.z)
        {
            transform.position = firstPos;
        }

    }

    private void GameIsOff()
    {
        currentSpeed = 0;
    }

    private void GameIsOn()
    {
        currentSpeed = speed;
    }

}
