using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : BaseUnitBehaviour
{
    /// <summary>
    /// Time in seconds.
    /// </summary>
    public int TimeBeforeExplosion = 10;

    /// <summary>
    /// Tag of units targeted by explosion.
    /// </summary>
    private string tagFocus;

    protected override void Init()
    {
        if (gameObject.tag == "PlayerUnit")
            tagFocus = "EnemyUnit";
        else
            tagFocus = "PlayerUnit";

        StartCoroutine(Explode());
        base.Init();
    }

    protected override void UpdateUnit() { }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(10);
        List<UnitController> units = findUnitsInRange(tagFocus);
        foreach (UnitController unit in units)
            controller.AttackUnit(unit);
        controller.Destroy();
    }
}
