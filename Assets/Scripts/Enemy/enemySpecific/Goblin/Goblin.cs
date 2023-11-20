using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Entity
{
    public Goblin_IdleState idleState { get; private set; }
    public Goblin_MoveState moveState { get; private set; }
    public Goblin_PlayerDetected playerDetectedState { get; private set; }
    public Goblin_ChargeState chargeState { get; private set; } 
    public Goblin_LookForPlayerState lookForPlayerState { get; private set; }
    public Goblin_MeleeAttackState meleeAttackState { get; private set; }
    public Goblin_StunState stunState { get; private set; }
    public Goblin_DeadState deadState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_MeleAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStatedata;
    [SerializeField] private D_DeadState deadStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new Goblin_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Goblin_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Goblin_PlayerDetected(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Goblin_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Goblin_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Goblin_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Goblin_StunState(this, stateMachine, "stun", stunStatedata, this);
        deadState = new Goblin_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Attack(attackDetails attackDetails)
    {
        base.Attack(attackDetails);

        if (isDead)
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
}
