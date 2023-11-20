using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_cloudAttack : MeleeAttackState
{
    private Shroom shroom;

    public Shroom_cloudAttack(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleAttackState stateData, Shroom shroom) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationfinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(shroom.playerDetectedState);
            }
            else if (!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(shroom.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
