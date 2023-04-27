using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour, IUnitBehaviour
{
    public NavMeshAgent Agent { get; set; }

    private List<BaseUnitController> unitsTargated = new List<BaseUnitController>();
    private List<Vector3> castlePositions = new List<Vector3>();
    private Vector3 castleDestination;
    private EnemyUnitController controller;

    public void Init(BaseUnitController unitController)
    {
        List<CastleController> castles = FindObjectsOfType<CastleController>().ToList();

        foreach (CastleController castle in castles)
            if (castle.gameObject != null)
                castlePositions.Add(castle.gameObject.transform.position);

        Agent = gameObject.GetComponent<NavMeshAgent>();
        controller = gameObject.GetComponent<EnemyUnitController>();
        Agent.stoppingDistance = controller.Range;

        if (castlePositions.Count > 0)
        {
            castleDestination = FindCloseCastle(gameObject.transform.position);
            Agent.SetDestination(castleDestination);
        }

        UnitEvent.OnUnitDie += UntargetUnit;
    }

    public void UpdateUnit(Vector3 unitPosition)
    {
        if (unitsTargated.Count > 0)
        {
            BaseUnitController unitController = FindCloseUnit(unitPosition);
            float closeUnitDistance = Vector3.Distance(unitPosition, unitController.gameObject.transform.position);

            if (Agent.destination != unitController.gameObject.transform.position)
                Agent.SetDestination(unitController.gameObject.transform.position);

            if (closeUnitDistance <= controller.Range)
                controller.AttackUnit(unitController);
        }
        else if (Agent.destination != castleDestination && castlePositions.Count > 0)
        {
            castleDestination = FindCloseCastle(unitPosition);
            Agent.SetDestination(castleDestination);
        }
    }

    private BaseUnitController FindCloseUnit(Vector3 unitPosition)
    {
        float closeUnitDistance = unitsTargated.Min(u => Vector3.Distance(unitPosition, u.gameObject.transform.position));
        return unitsTargated.Find(u => Vector3.Distance(unitPosition, u.gameObject.transform.position) == closeUnitDistance);
    }

    private Vector3 FindCloseCastle(Vector3 unitPosition)
    {
        float closeUnitDistance = castlePositions.Min(u => Vector3.Distance(unitPosition, u));
        return castlePositions.Find(u => Vector3.Distance(unitPosition, u) == closeUnitDistance);
    }

    public void OnUnitDestroy()
    {
        UnitEvent.OnUnitDie -= UntargetUnit;
    }

    public void TargetUnit(BaseUnitController unitController)
    {
        if (!unitsTargated.Contains(unitController))
            unitsTargated.Add(unitController);
    }

    public void UntargetUnit(BaseUnitController unitController)
    {
        unitsTargated.Remove(unitController);
    }
}
