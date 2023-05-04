using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitController : MonoBehaviour
{
    [Header("Stats")]
    public int HP = 10;
    public int Attack = 0;
    public float RangeDetection = 1;
    public float RangeAttack = 1;


    private void OnDestroy()
    {
        onUnitDie();
    }

    public virtual void ReceiveDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
            Destroy(gameObject);
    }

    protected virtual void onUnitDie()
    {
        UnitEvent.UnitDie(this);
    }
}
