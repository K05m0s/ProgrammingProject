using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_IdleState : IdleState
{
    private Shroom shroom;

    public Shroom_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, stateData)
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

        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(shroom.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
