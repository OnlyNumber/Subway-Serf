using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private PlayerController myPlayer; 

    private Vector2 tapPosition;
    private Vector2 secondTap;

    private bool isMobile;
    private bool isSwiping;

    [SerializeField]
    private float checkZone; 

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
        myPlayer = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!isMobile)
        {

            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPosition = Input.mousePosition;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }

        CheckPos();
    }

    private void CheckPos()
    {
        secondTap = Vector2.zero;

        if (isSwiping)
        {

            if (!isMobile && Input.GetMouseButton(0))
            {
                secondTap = (Vector2)Input.mousePosition - tapPosition;
            }

        }


        if(secondTap.magnitude > checkZone)
        {
            if(Mathf.Abs(secondTap.x) > Mathf.Abs(secondTap.y))
            {

                if(secondTap.x > 0)
                {
                    myPlayer.MoveHorizontal(6f);
                }
                else
                {
                    myPlayer.MoveHorizontal(-6f);
                }
            }
            else
            {
                if (secondTap.y > 0)
                {
                    myPlayer.Jump();
                }
                else
                {

                    myPlayer.SlideInvoker();

                }
            }

            ResetSwipe();
        }
        

    }

    private void ResetSwipe()
    {
        isSwiping = false;

        tapPosition = Vector2.zero;
        secondTap = Vector2.zero;
    }




}
