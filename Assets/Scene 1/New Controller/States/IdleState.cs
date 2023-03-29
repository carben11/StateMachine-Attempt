using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    protected int xInput;
    protected int yInput;
    public IdleState(Player player, PlayerStateMachine StateMachine, PlayerData playerData, string animBoolName) : base(player, StateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0f);

        //Debug.Log("Idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        yInput = player.InputHandler.NormInputY;


        if (xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }


        if (yInput > 0.5f)
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
