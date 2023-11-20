using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_PDS : PlayerDetectedState
{
    private Shroom shroom;

    public Shroom_PDS(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(shroom.cloudAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(shroom.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
