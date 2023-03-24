using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    private State currentState;

    [SerializeField]
    private Animator animController;

    private void Start()
    {
        currentState = new Idle();
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void MainState()
    {
        currentState.OnExit();
        
        currentState = new Running();

        currentState.OnEnter();
    }

    public void GoToNextState(State nextState)
    {
        currentState.OnExit();

        currentState = nextState;

        currentState.OnEnter();


    }



}
