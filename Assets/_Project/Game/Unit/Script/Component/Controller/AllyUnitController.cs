using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyUnitController : UnitController
{
    // Start is called before the first frame update
    private void Awake()
    {
        if(!TryGetComponent(out Behaviour))
            gameObject.AddComponent<AllyUnitBehaviour>();
    }

    public void setProperty(int hp, int attack, float rangeDetection, float rangeAttack)
    {
        HP = hp;
        Attack = attack;
        RangeDetection = rangeDetection;
        RangeAttack = rangeAttack;
    }
}
