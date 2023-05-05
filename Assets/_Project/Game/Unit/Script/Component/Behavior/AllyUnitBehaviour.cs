using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AllyUnitBehaviour : BaseUnitBehaviour
{
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
