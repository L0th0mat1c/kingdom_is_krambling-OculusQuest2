using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : BaseUnitController, IUnitAttacker
{
    [Header("Unit components")]
    [SerializeField]private EnemyBehavior enemyBehavior;
    [SerializeField]private UnitAttack unitAttack;

    public void AttackUnit(BaseUnitController unitController)
    {
        if (unitAttack != null)
            unitAttack.Attack(unitController);
    }
}
