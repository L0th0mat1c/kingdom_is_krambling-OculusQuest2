using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyUnitController : UnitController
{
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.AddComponent<AllyUnitBehaviour>();
    }
}
