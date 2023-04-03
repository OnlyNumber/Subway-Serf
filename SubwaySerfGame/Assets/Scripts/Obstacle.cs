using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;

    [SerializeField]
    private float currentSpeed;

    public float Speed
    {
        set
        {
            if(value > 0)
            {
                speed = value;
            }
        }
        get
        {
            return speed;
        }


    }


    [SerializeField]
    private float positionForDeath;

    Rigidbody myRb;

    private void Start()
    {
        currentSpeed = speed;

        myRb = GetComponent<Rigidbody>();

        SpawnManager.instance.onIsGameFinish += GameIsOff;
        SpawnManager.instance.onIsGameStart += GameIsOn;

    }

    void Update()
    {
        
        myRb.velocity = new Vector3(0, 0, -currentSpeed);

        if(transform.position.z <= positionForDeath)
        {
            gameObject.SetActive(false);
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


    public void OffObject()
    {

    }

}
