using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitAttack
{
    public void Init(BaseUnitController unitController);
    public void AttackUnit(BaseUnitController unitController);
}
