using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurretBehaviour : BaseUnitBehaviour
{
    private string tagFocus;

    protected override void Init()
    {
        base.Init();
        if (tag == "PlayerUnit")
            tagFocus = "EnemyUnit";
        else
            tagFocus = "PlayerUnit";
    }

    protected override void UpdateUnit()
    {
        List<UnitController> controllers = FindObjectsOfType<UnitController>().Where(c => Vector3.Distance(transform.position, c.gameObject.transform.position) <= controller.RangeAttack).ToList();
        if (controllers.Count == 0)
            return;
        controllers.GroupBy(c => Vector3.Distance(transform.position, c.gameObject.transform.position));
        attack(controllers.First());
    }

    private void attack(UnitController enemy)
    {
        controller.AttackUnit(enemy);
    }
}
