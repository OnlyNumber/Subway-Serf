using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float currentSpeed;

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
        
        myRb.velocity = new Vector3(0, 0, -speed);

        if(transform.position.z <= positionForDeath)
        {

            //SpawnManager.instance.onIsGameFinish -= GameIsOff;

            gameObject.SetActive(false);

            //Destroy(gameObject);
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


    public void OffObject()
    {

    }

}
