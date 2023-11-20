using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newmeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 25f;
    public LayerMask playerLayer;
}
