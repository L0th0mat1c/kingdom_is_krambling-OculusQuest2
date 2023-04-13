using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public SphereCollider Collider;
    public List<BaseUnitController> UnitsTargated = new List<BaseUnitController>();

    private Vector3 castlePosition;
    private NavMeshAgent agent;
    private UnitController controller;

    private void Start()
    {
        GameObject castle = GameObject.Find("Castle");

        if (castle == null)
            throw new System.Exception("Castle not found");

        castlePosition = castle.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        controller = gameObject.GetComponent<UnitController>();

        agent.SetDestination(castlePosition);
        agent.stoppingDistance = controller.Range;
    }

    private void Update()
    {
        DeleteUnitsDead();

        if (UnitsTargated.Count > 0)
        {
            BaseUnitController unitController = FindCloseUnit();
            float closeUnitDistance = Vector3.Distance(gameObject.transform.position, unitController.gameObject.transform.position);

            if (agent.destination != unitController.gameObject.transform.position)
                agent.SetDestination(unitController.gameObject.transform.position);

            if (closeUnitDistance <= controller.Range)
                controller.AttackUnit(unitController);
        }
        else if(agent.destination != castlePosition)
        {
            agent.SetDestination(castlePosition);
        }
    }

    /// <summary>
    /// Supprime les unités mortes (= UnitController où gameObject est null ou les valeurs null).
    /// </summary>
    private void DeleteUnitsDead()
    {
        List<BaseUnitController> unitsDeadOrNull = UnitsTargated.FindAll(u => {
            if (u != null)
                if (u.gameObject != null)
                    return false;
            return true;
        });

        foreach (BaseUnitController unitDead in unitsDeadOrNull)
            UnitsTargated.Remove(unitDead);
    }

    private BaseUnitController FindCloseUnit()
    {
        float closeUnitDistance = UnitsTargated.Min(u => Vector3.Distance(gameObject.transform.position, u.gameObject.transform.position));
        return UnitsTargated.Find(u => Vector3.Distance(gameObject.transform.position, u.gameObject.transform.position) == closeUnitDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerUnit")
        {
            BaseUnitController unitController;
            if(other.TryGetComponent(out unitController))
            {
                UnitsTargated.Add(unitController);
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
                UnitsTargated.Remove(unitController);
                if (unitController is CastleController)
                    Debug.Log("Castle exit");
            }
        }
    }
}
