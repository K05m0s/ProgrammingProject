using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_StunState : StunState
{
    private Shroom shroom;

    public Shroom_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isStunTimeOver)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(shroom.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(shroom.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
