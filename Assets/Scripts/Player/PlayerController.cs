using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator animController;

    //private SpawnManager spMan;

    private Rigidbody myRb;

    [SerializeField]
    private BoxCollider upCollider;

    public BoxCollider UpCollider
    {
        private set
        {
            upCollider = value;
        }
        get
        {
            return upCollider;
        }
    }

    [SerializeField]
    private BoxCollider downCollider;

    [SerializeField]
    private float slideTime;

    [SerializeField]
    private Transform[] lines;

    private Vector3 selectedLine;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private float downPower;

    [SerializeField]
    private float speed;

    private void Start()
    {

        selectedLine = new Vector3(0, 0, -2.2f);

        myRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        selectedLine.y = transform.position.y;
        MovingToLine();

    }


    public void MoveHorizontal(float direction)
    {
        if (direction < 0)
        {

            if (selectedLine.x > -2)
            {
                selectedLine.x += -3;
            }
        }
        else
        {
            if (selectedLine.x < 2)
            {
                selectedLine.x += 3;
            }
        }
    }    

    private IEnumerator Slide ()
    {
        upCollider.enabled = false;

        downCollider.enabled = true;

        myRb.velocity = new Vector2(myRb.velocity.x, -downPower);

        yield return new WaitForSeconds(slideTime);

        upCollider.enabled = true;

    }

    public void Jump()
    {
        if (CheckGround())
        {
            //StopAllCoroutines();
            
            myRb.velocity = new Vector2(myRb.velocity.x, jumpPower);

            StartCoroutine(OffBoxCollider());
        }
    }

    private IEnumerator OffBoxCollider()
    {
        downCollider.enabled = false;

        yield return new WaitForSeconds(0.5f);

        downCollider.enabled = true;

    }

    public void SlideInvoker()
    {
        StopAllCoroutines();

        StartCoroutine(Slide());
    }

    private void MovingToLine()
    {
        if (Mathf.Abs(Mathf.Abs(selectedLine.x) - Mathf.Abs(transform.position.x)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, selectedLine, speed * Time.deltaTime);
        }
    }

    public bool CheckGround()
    {

        Collider[] groundHits = Physics.OverlapSphere(transform.position, 0.1f);

        foreach(var hit in groundHits)
        {
            if(hit.CompareTag("Ground"))
            {
                return true;
            }    
        }

        return false;
    
    }

    public void AfterDeath()
    {
        selectedLine.x = 0;

        transform.position = new Vector3(0, 0, transform.position.z);

    }



}
