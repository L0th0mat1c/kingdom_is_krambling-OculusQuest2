using UnityEngine;
using UnityEngine.AI;

public interface IUnitBehaviour
{
    public NavMeshAgent Agent { get; set; }
    public void Init(BaseUnitController unitController);
    public void OnUnitDestroy();
    public void UpdateUnit(Vector3 unitPosition);
    public void TargetUnit(BaseUnitController unitController);
    public void UntargetUnit(BaseUnitController unitController);
}
