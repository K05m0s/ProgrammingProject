using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_MoveState : MoveState
{
    private Goblin goblin;

    public Goblin_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
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
            stateMachine.ChangeState(goblin.playerDetectedState);
        }

        if (isDetectingWall || !isDetectingLedge)
        {
            goblin.idleState.setFlipAfterIdle(true);
            stateMachine.ChangeState(goblin.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
