using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AllyUnitBehaviour : BaseUnitBehaviour
{
    protected override void Init()
    {
        if (controller.RangeAttack - 0.5f >= 1)
            agent.stoppingDistance = controller.RangeAttack - 0.5f;
        else
            agent.stoppingDistance = 1;

        base.Init();
    }

    protected override void UpdateUnit()
    {
        UnitController closeUnit = findCloseUnit(gameObject.transform.position, "EnemyUnit");
        if (closeUnit == null || agent == null)
            return;
        if (!agent.isActiveAndEnabled)
            return;

        float closeUnitDistance = Vector3.Distance(gameObject.transform.position, closeUnit.transform.position);
        if (closeUnitDistance <= controller.RangeDetection)
        {
            if(closeUnitDistance > controller.RangeAttack && agent.destination != closeUnit.transform.position)
                agent.SetDestination(closeUnit.gameObject.transform.position);
            if(closeUnitDistance <= controller.RangeAttack)
                controller.AttackUnit(closeUnit);
        }
    }
}
