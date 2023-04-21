using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyUnitController : BaseUnitController, IUnitAttacker
{
    public IUnitBehaviour UnitBehaviour;
    public IUnitAttack UnitAttack;

    // Start is called before the first frame update
    private void Start()
    {
        UnitBehaviour = new AllyUnitBehaviour();
        UnitAttack = new AllyUnitAttack();

        if(UnitBehaviour != null)
        {
            NavMeshAgent agent;
            if (UnitBehaviour.CanMove && gameObject.TryGetComponent(out agent)) {
                UnitBehaviour.Agent = agent;
            }

            UnitBehaviour.Init(this);
            InvokeRepeating("behaviourUpdate", 1f, 0.5f);
        }

        if(UnitAttack != null)
            UnitAttack.Init(this);
    }

    public void AttackUnit(BaseUnitController unitController)
    {
        if (UnitAttack != null)
            UnitAttack.AttackUnit(unitController);
    }

    // Update is called once per frame
    private void behaviourUpdate()
    {
        UnitBehaviour.Update(gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyUnit" && UnitBehaviour != null)
        {
            BaseUnitController enemyUnitController;
            if(other.TryGetComponent(out enemyUnitController))
            {
                UnitBehaviour.TargetUnit(enemyUnitController);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyUnit" && UnitBehaviour != null)
        {
            BaseUnitController enemyUnitController;
            if (other.TryGetComponent(out enemyUnitController))
            {
                UnitBehaviour.UntargetUnit(enemyUnitController);
            }
        }
    }

    private void OnDestroy()
    {
        if (UnitBehaviour != null)
            UnitBehaviour.OnDestroy();
    }
}
