using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface qui permet à une unité d'attaqué. A hérité dans le controller.
/// </summary>
public interface IUnitAttacker
{
    /// <summary>
    /// Attaque l'unité en paramètre.
    /// </summary>
    /// <param name="unitController">L'unité à attaqué</param>
    public void AttackUnit(BaseUnitController unitController);
}
