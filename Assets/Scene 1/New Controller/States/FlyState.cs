using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

public class FlyState : PlayerState
{
    protected int xInput;
    protected int yInput;
    public FlyState(Player player, PlayerStateMachine StateMachine, PlayerData playerData, string animBoolName) : base(player, StateMachine, playerData, animBoolName)
    {

    }

    public float flyTime { get; private set; }
    public bool canFly { get; private set; }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        flyTime = playerData.totalFlyTime;

        //Debug.Log("Jump");
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

        if (canFly && yInput > .5f)
        {
            //time count down here
            //player.Fly();
        }

        if (player.CurrentVelocity.y <= .1f)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
