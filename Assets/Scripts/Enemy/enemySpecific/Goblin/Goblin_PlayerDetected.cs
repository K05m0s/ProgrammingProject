using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_PlayerDetected : PlayerDetectedState
{
    private Goblin goblin;

    public Goblin_PlayerDetected(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(goblin.meleeAttackState);
        }

        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(goblin.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(goblin.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
