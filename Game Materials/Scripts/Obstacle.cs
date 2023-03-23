using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float positionForDeath;

    Rigidbody myRb;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myRb.velocity = new Vector3(0, 0, -speed);

        Debug.Log(transform.position);

        if(transform.position.z <= positionForDeath)
        {
            Destroy(gameObject);
        }


    }
}
