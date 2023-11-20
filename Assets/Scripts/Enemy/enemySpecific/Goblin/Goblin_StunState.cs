using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_StunState : StunState
{
    private Goblin goblin;

    public Goblin_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Goblin goblin) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.goblin = goblin;
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

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(goblin.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(goblin.chargeState);
            }
            else
            {
                goblin.lookForPlayerState.SetFlipImmediatley(true);
                stateMachine.ChangeState(goblin.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
