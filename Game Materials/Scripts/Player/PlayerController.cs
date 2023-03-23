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

    [SerializeField]
    private float slideTime;

    private Vector3 MovingLine;

    [SerializeField]
    private Transform[] lines;

    private Vector3 selectedLine;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private float speed;

    public bool isDead = false;


    private void Start()
    {


        selectedLine = new Vector3(0, 0, -2.2f);

        SpawnManager.instance.onIsGameFinish += Death;
        SpawnManager.instance.onIsGameStart += StartGame;


        myRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animController.SetBool("isGround", CheckGround());
        //Debug.Log(CheckGround());

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
        animController.SetBool("isSliding", true);


        myRb.velocity = new Vector2(myRb.velocity.x, -jumpPower/1.5f);

        yield return new WaitForSeconds(slideTime);

        animController.SetBool("isSliding", false);

        upCollider.enabled = true;

    }

    public void Jump()
    {
        if (CheckGround())
        {
            //Debug.Log("JUMP");
            myRb.velocity = new Vector2(myRb.velocity.x, jumpPower);
        }
    }

    public void SlideInvoker()
    {
        StartCoroutine(Slide());
    }

    private void MovingToLine()
    {
        if (Mathf.Abs(Mathf.Abs(selectedLine.x) - Mathf.Abs(transform.position.x)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, selectedLine, speed * Time.deltaTime);
        }
    }

    private bool CheckGround()
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

    private void StartGame()
    {
        animController.SetTrigger("StartGame");
    }

    private void Death()
    {
        animController.SetBool("isDead", true);
    }


}
