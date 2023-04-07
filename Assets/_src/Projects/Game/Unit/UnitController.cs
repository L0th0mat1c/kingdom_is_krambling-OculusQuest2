using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int HP = 10;
    public int Attack = 0;
    public float Range = 1;

    private EnemyBehavior enemyBehavior;
    private UnitAttack unitAttack;

    private void Awake()
    {
        gameObject.TryGetComponent(out enemyBehavior);
        gameObject.TryGetComponent(out unitAttack);
    }

    public void AttackUnit(UnitController unitController)
    {
        if(unitAttack != null)
        {
            if (unitAttack.Attack(unitController) && enemyBehavior != null)
                enemyBehavior.UnitsTargated.Remove(unitController);
        }
    }
}
