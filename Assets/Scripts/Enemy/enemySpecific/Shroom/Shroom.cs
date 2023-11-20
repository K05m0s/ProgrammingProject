using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : Entity
{
    public Shroom_MoveState moveState { get; private set; }
    public Shroom_IdleState idleState { get; private set; }
    public Shroom_PDS playerDetectedState { get; private set; }
    public Shroom_cloudAttack cloudAttackState { get; private set; }
    public Shroom_LookForPlayerState lookForPlayerState { get; private set; }
    public Shroom_StunState stunState { get; private set; }
    public Shroom_DeadState deadState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_MeleAttackState cloudAttackData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;

    [SerializeField] private Transform cloudAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new Shroom_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Shroom_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Shroom_PDS(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        cloudAttackState = new Shroom_cloudAttack(this, stateMachine, "attack",cloudAttackPosition, cloudAttackData, this);
        lookForPlayerState = new Shroom_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new Shroom_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Shroom_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Attack(attackDetails attackDetails)
    {
        base.Attack(attackDetails);

        if(isDead)
        {
            stateMachine.ChangeState(deadState);
        }

        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetFlipImmediatley(true);
            stateMachine.ChangeState(lookForPlayerState);
        }

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(cloudAttackPosition.position, cloudAttackData.attackRadius);
    }
}
