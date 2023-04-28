using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AllyUnitBehaviour : IUnitBehaviour
{
    private List<BaseUnitController> unitsTargeted = new List<BaseUnitController>();
    private AllyUnitController allyUnitController;
    public NavMeshAgent Agent { get; set; }
    public bool CanMove { get; } = true;

    public void Init(BaseUnitController unitController) 
    {
        allyUnitController = (AllyUnitController)unitController;
        UnitEvent.OnUnitDie += UntargetUnit;
    }

    public void OnUnitDestroy()
    {
        UnitEvent.OnUnitDie -= UntargetUnit;
    }

    public void TargetUnit(BaseUnitController unitController)
    {
        if(!unitsTargeted.Contains(unitController))
            unitsTargeted.Add(unitController);
    }

    public void UntargetUnit(BaseUnitController unitController)
    {
        unitsTargeted.Remove(unitController);
    }

    public void UpdateUnit(Vector3 unitPosition)
    {
        if(unitsTargeted.Count > 0)
        {
            BaseUnitController closeUnit = FindCloseUnit(unitPosition);

            if(Agent != null)
                if(Agent.isActiveAndEnabled)
                    Agent.SetDestination(closeUnit.gameObject.transform.position);
            if(Vector3.Distance(unitPosition, closeUnit.gameObject.transform.position) <= allyUnitController.Range)
                allyUnitController.AttackUnit(closeUnit);
        }
    }

    private BaseUnitController FindCloseUnit(Vector3 unitPosition)
    {
        float closeUnitDistance = unitsTargeted.Min(u => Vector3.Distance(unitPosition, u.gameObject.transform.position));
        return unitsTargeted.Find(u => Vector3.Distance(unitPosition, u.gameObject.transform.position) == closeUnitDistance);
    }
}
