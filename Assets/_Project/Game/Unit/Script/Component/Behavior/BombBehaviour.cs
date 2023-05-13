using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : BaseUnitBehaviour
{
    public string TagFocus;

    protected override void Init()
    {
        if (gameObject.tag == "PlayerUnit")
            TagFocus = "EnemyUnit";
        else
            TagFocus = "PlayerUnit";

        StartCoroutine(Explode());
        base.Init();
    }
    protected override void UpdateUnit() { }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("BOOUM !!!");
        List<UnitController> units = findUnitsInRange(TagFocus);
        foreach (UnitController unit in units)
            controller.AttackUnit(unit);
        Destroy(gameObject);
    }
}
