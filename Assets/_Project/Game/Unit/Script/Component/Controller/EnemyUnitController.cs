using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : BaseUnitController
{
    [Header("Unit components")]
    [SerializeField]private EnemyBehavior enemyBehavior;

    private UnitAttack unitAttack;

    private void Start()
    {
        unitAttack = new UnitAttack();
        unitAttack.Init(this);

        if (enemyBehavior != null)
        {
            enemyBehavior.Init(this);
            InvokeRepeating("behaviourUpdate", 1f, 0.5f);
        }
    }

    public void AttackUnit(BaseUnitController unitController)
    {
        if (unitAttack != null)
            unitAttack.AttackUnit(unitController);
    }

    private void behaviourUpdate()
    {
        if (enemyBehavior != null)
            enemyBehavior.UpdateUnit(gameObject.transform.position);
    }
}
