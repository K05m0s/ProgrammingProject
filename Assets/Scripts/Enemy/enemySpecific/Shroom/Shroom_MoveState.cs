using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_MoveState : MoveState
{
    private Shroom shroom;

    public Shroom_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.shroom = shroom;
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(shroom.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            shroom.idleState.setFlipAfterIdle(true);
            stateMachine.ChangeState(shroom.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
