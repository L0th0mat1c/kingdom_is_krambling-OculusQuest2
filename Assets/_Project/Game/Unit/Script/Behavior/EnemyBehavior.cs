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
        if (controller.RangeAttack - 0.5f >= 1)
            Agent.stoppingDistance = controller.RangeAttack - 0.5f;
        else
            Agent.stoppingDistance = 1;

        if (castlePositions.Count > 0 && Agent.isActiveAndEnabled)
        {
            castleDestination = FindCloseCastle(gameObject.transform.position);
            Agent.SetDestination(castleDestination);
        }

        UnitEvent.OnUnitDie += UntargetUnit;
    }

    public void UpdateUnit(Vector3 unitPosition)
    {
        BaseUnitController unitController = FindCloseUnit(gameObject.transform.position);

        if (unitController == null)
            SetCastleDestination(gameObject.transform.position);

        float closeUnitDistance = Vector3.Distance(gameObject.transform.position, unitController.gameObject.transform.position);
        if (closeUnitDistance <= controller.RangeDetection)
        {
            if (closeUnitDistance > controller.RangeAttack && Agent.destination != unitController.gameObject.transform.position)
            {
                Agent.SetDestination(unitController.gameObject.transform.position);
                Debug.Log($"Go to unit - {closeUnitDistance}");
            }

            if (closeUnitDistance <= controller.RangeAttack)
            {
                controller.AttackUnit(unitController);
                Debug.Log($"Attack ! {closeUnitDistance}");
            }
        }
        else if (Agent.destination != castleDestination && castlePositions.Count > 0)
        {
            SetCastleDestination(gameObject.transform.position);
        }
    }

    private BaseUnitController FindCloseUnit(Vector3 unitPosition)
    {
        List<BaseUnitController> units = FindObjectsOfType<BaseUnitController>().ToList();
        Debug.Log(units.Count);
        units.Remove(controller);
        if (units.Count == 0)
            return null;

        float closeUnitDistance = -1;
        BaseUnitController closeUnit = null;
        foreach (BaseUnitController unit in units)
        {
            float distance = Vector3.Distance(unitPosition, unit.gameObject.transform.position);
            if(distance < closeUnitDistance || closeUnitDistance < 0)
            {
                closeUnitDistance = distance;
                closeUnit = unit;
            }
        }
        return closeUnit;
    }

    private Vector3 FindCloseCastle(Vector3 unitPosition)
    {
        float closeUnitDistance = castlePositions.Min(u => Vector3.Distance(unitPosition, u));
        return castlePositions.Find(u => Vector3.Distance(unitPosition, u) == closeUnitDistance);
    }

    private void SetCastleDestination(Vector3 unitPosition)
    {
        castleDestination = FindCloseCastle(unitPosition);
        Agent.SetDestination(castleDestination);
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
