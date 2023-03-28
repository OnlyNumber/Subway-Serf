using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    [SerializeField]
    private State currentState;

    public Animator animController;

    

    public PlayerController playerControl;

    private void Start()
    {
        instance = this;
        animController = GetComponentInChildren<Animator>();
        playerControl = GetComponent<PlayerController>();
        currentState = new Idle();
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void IdleState()
    {
        currentState.OnExit();
        
        currentState = new Idle();

        currentState.OnEnter();
    }

    public void GoToNextState(State nextState)
    {
        currentState.OnExit();

        currentState = nextState;

        currentState.OnEnter();

        Debug.Log(nextState.ToString());



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Death");

            GoToNextState(new Dead());
        }
    }

}
