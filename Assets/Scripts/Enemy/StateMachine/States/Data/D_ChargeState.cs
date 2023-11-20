using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge Data")]
public class D_ChargeState : ScriptableObject
{
    public float chargeSpeed = 8f;
    public float chargeTime = 2f;
}
