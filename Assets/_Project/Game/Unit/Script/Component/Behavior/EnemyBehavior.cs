using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : BaseUnitBehaviour
{
    private List<Vector3> castlePositions = new List<Vector3>();
    private Vector3 castleDestination;

    protected override void Init()
    {
        List<CastleController> castles = FindObjectsOfType<CastleController>().ToList();

        foreach (CastleController castle in castles)
            if (castle.gameObject != null)
                castlePositions.Add(castle.gameObject.transform.position);

        if (controller.RangeAttack - 0.5f >= 1)
            agent.stoppingDistance = controller.RangeAttack - 0.5f;
        else
            agent.stoppingDistance = 1;

        if (castlePositions.Count > 0 && agent.isActiveAndEnabled)
        {
            castleDestination = findCloseCastle(gameObject.transform.position);
            agent.SetDestination(castleDestination);
        }

        base.Init();
    }

    protected override void UpdateUnit()
    {
        UnitController unitController = findCloseUnit(gameObject.transform.position, "PlayerUnit");

        if (unitController == null)
            setCastleDestination(gameObject.transform.position);

        float closeUnitDistance = Vector3.Distance(gameObject.transform.position, unitController.gameObject.transform.position);
        if (closeUnitDistance <= controller.RangeDetection)
        {
            if (closeUnitDistance > controller.RangeAttack && agent.destination != unitController.gameObject.transform.position)
                agent.SetDestination(unitController.gameObject.transform.position);

            if (closeUnitDistance <= controller.RangeAttack)
                controller.AttackUnit(unitController);
        }
        else if (agent.destination != castleDestination && castlePositions.Count > 0)
        {
            setCastleDestination(gameObject.transform.position);
        }
    }

    private Vector3 findCloseCastle(Vector3 unitPosition)
    {
        float closeUnitDistance = castlePositions.Min(u => Vector3.Distance(unitPosition, u));
        return castlePositions.Find(u => Vector3.Distance(unitPosition, u) == closeUnitDistance);
    }

    private void setCastleDestination(Vector3 unitPosition)
    {
        castleDestination = findCloseCastle(unitPosition);
        agent.SetDestination(castleDestination);
    }
}
