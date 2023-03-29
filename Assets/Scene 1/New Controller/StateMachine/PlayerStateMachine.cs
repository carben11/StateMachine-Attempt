using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)   //Enters starting state
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)   //Exits current state and enters new state
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
