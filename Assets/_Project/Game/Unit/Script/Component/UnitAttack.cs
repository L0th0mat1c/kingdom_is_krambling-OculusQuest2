using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : IUnitAttack
{
    private BaseUnitController unitController;

    public void Init(BaseUnitController unit)
    {
        unitController = unit;
    }

    public void AttackUnit(BaseUnitController unit)
    {
        unit.ReceiveDamage(unitController.Attack);
    }
}
