using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnitAttack : IUnitAttack
{
    private AllyUnitController allyUnitController;

    public void Init(BaseUnitController unitController)
    {
        allyUnitController = (AllyUnitController)unitController;
    }

    public void AttackUnit(BaseUnitController unitController)
    {
        unitController.ReceiveDamage(allyUnitController.Attack);
    }
}
