using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseUnitBehaviour : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected UnitController controller;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if(controller == null)
            controller = GetComponent<UnitController>();
        Init();
    }

    protected abstract void UpdateUnit();

    protected virtual void Init()
    {
        InvokeRepeating("UpdateUnit", 1f, 0.5f);
    }

    protected UnitController findCloseUnit(Vector3 unitPosition, string tagFocus)
    {
        List<UnitController> units = FindObjectsOfType<UnitController>().ToList();
        units.RemoveAll(u => u.gameObject.tag != tagFocus || u == controller);
        if (units.Count == 0)
            return null;

        float closeUnitDistance = -1;
        UnitController closeUnit = null;
        foreach (UnitController unit in units)
        {
            float distance = Vector3.Distance(unitPosition, unit.gameObject.transform.position);
            if (distance < closeUnitDistance || closeUnitDistance < 0)
            {
                closeUnitDistance = distance;
                closeUnit = unit;
            }
        }
        return closeUnit;
    }
}
