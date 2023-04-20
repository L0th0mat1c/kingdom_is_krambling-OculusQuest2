using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : StructureController
{
    public override void ReceiveDamage(int damage)
    {
        if(HP - damage <= 0)
            Debug.Log("LOOSE !");
        base.ReceiveDamage(damage);
    }
}