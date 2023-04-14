using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface qui permet � une unit� d'attaqu�. A h�rit� dans le controller.
/// </summary>
public interface IUnitAttacker
{
    /// <summary>
    /// Attaque l'unit� en param�tre.
    /// </summary>
    /// <param name="unitController">L'unit� � attaqu�</param>
    public void AttackUnit(BaseUnitController unitController);
}
