using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : StructureController
{
    public override void ReceiveDamage(int damage)
    {
        Debug.Log("LOOSE !");
    }
}
