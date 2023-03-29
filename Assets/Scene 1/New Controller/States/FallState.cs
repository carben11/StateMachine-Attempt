using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

public class FallState : PlayerState
{
    protected int xInput;
    protected int yInput;
    public FallState(Player player, PlayerStateMachine StateMachine, PlayerData playerData, string animBoolName) : base(player, StateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckShouldFlip(xInput);

        player.Fall();

        player.SetVelocityX(playerData.movementSpeed * xInput);

        yInput = player.InputHandler.NormInputY;

        xInput = player.InputHandler.NormInputX;


        if (yInput > .5f)
        {
            if (player.canJump)
            {
                player.Jump();
            }
            else if (playerData.canFly)
            {
                stateMachine.ChangeState(player.FlyState);
            }
        }

        if (player.onGround && xInput == 0f)
         {
            stateMachine.ChangeState(player.IdleState);
         }

        if (player.onGround && xInput != 0f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
