using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private List<BaseUnitController> unitsTargated = new List<BaseUnitController>();
    private List<Vector3> castlePositions = new List<Vector3>();
    private Vector3 castleDestination;
    private NavMeshAgent agent;
    private UnitController controller;

    private void Start()
    {
        List<CastleController> castles = FindObjectsOfType<CastleController>().ToList();

        foreach (CastleController castle in castles)
            if (castle.gameObject != null)
                castlePositions.Add(castle.gameObject.transform.position);

        agent = gameObject.GetComponent<NavMeshAgent>();
        controller = gameObject.GetComponent<UnitController>();
        agent.stoppingDistance = controller.Range;

        if(castlePositions.Count > 0)
        {
            castleDestination = FindCloseCastle();
            agent.SetDestination(castleDestination);
        }

        UnitEvent.OnUnitDie += RemoveUnitDead;
    }

    private void Update()
    {
        if (unitsTargated.Count > 0)
        {
            BaseUnitController unitController = FindCloseUnit();
            float closeUnitDistance = Vector3.Distance(gameObject.transform.position, unitController.gameObject.transform.position);

            if (agent.destination != unitController.gameObject.transform.position)
                agent.SetDestination(unitController.gameObject.transform.position);

            if (closeUnitDistance <= controller.Range)
                controller.AttackUnit(unitController);
        }
        else if(agent.destination != castleDestination && castlePositions.Count > 0)
        {
            castleDestination = FindCloseCastle();
            agent.SetDestination(castleDestination);
        }
    }

    private void RemoveUnitDead(BaseUnitController unitController)
    {
        if (unitsTargated.Contains(unitController))
        {
            unitsTargated.Remove(unitController);
            if (unitController is CastleController && unitController.gameObject != null)
                castlePositions.Remove(unitController.gameObject.transform.position);
        }
    }

    private BaseUnitController FindCloseUnit()
    {
        float closeUnitDistance = unitsTargated.Min(u => Vector3.Distance(gameObject.transform.position, u.gameObject.transform.position));
        return unitsTargated.Find(u => Vector3.Distance(gameObject.transform.position, u.gameObject.transform.position) == closeUnitDistance);
    }

    private Vector3 FindCloseCastle()
    {
        float closeUnitDistance = castlePositions.Min(u => Vector3.Distance(gameObject.transform.position, u));
        return castlePositions.Find(u => Vector3.Distance(gameObject.transform.position, u) == closeUnitDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerUnit")
        {
            BaseUnitController unitController;
            if(other.TryGetComponent(out unitController))
            {
                unitsTargated.Add(unitController);
                if (unitController is CastleController)
                    Debug.Log("Castle enter");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerUnit")
        {
            BaseUnitController unitController;
            if (other.TryGetComponent(out unitController))
            {
                unitsTargated.Remove(unitController);
                if (unitController is CastleController)
                    Debug.Log("Castle exit");
            }
        }
    }

    private void OnDestroy()
    {
        UnitEvent.OnUnitDie -= RemoveUnitDead;
    }
}
