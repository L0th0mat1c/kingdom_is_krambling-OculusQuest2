using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnitController : MonoBehaviour
{
    [Header("Stats")]
    public int HP = 10;
    public int Attack = 0;
    public float Range = 1;

    private void OnDestroy()
    {
        UnitEvent.UnitDie(this);
    }

    public virtual void ReceiveDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
            Destroy(gameObject);
    }
}
