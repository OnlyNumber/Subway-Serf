using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;

    Rigidbody myRb;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        myRb.velocity = new Vector3(0, 0, -speed);
    }
}
