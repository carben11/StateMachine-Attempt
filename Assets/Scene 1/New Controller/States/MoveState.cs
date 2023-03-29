using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    protected int xInput;
    protected int yInput;
    public MoveState(Player player, PlayerStateMachine StateMachine, PlayerData playerData, string animBoolName) : base(player, StateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Move");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckShouldFlip(xInput);

        player.SetVelocityX(playerData.movementSpeed * xInput);

        yInput = player.InputHandler.NormInputY;

        xInput = player.InputHandler.NormInputX;


        if (xInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (yInput > .5f)
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
