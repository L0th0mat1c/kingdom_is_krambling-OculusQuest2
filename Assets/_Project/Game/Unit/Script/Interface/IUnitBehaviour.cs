using UnityEngine;
using UnityEngine.AI;

public interface IUnitBehaviour
{
    public bool CanMove { get; }
    public NavMeshAgent Agent { get; set; }
    public void Init(BaseUnitController unitController);
    public void OnDestroy();
    public void Update(Vector3 unitPosition);
    public void TargetUnit(BaseUnitController unitController);
    public void UntargetUnit(BaseUnitController unitController);
}
