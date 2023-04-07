using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private UnitController controller;

    private void Start()
    {
        controller = gameObject.GetComponent<UnitController>();
    }

    /// <summary>
    /// Attaque l'unité en paramètre et retourne true si elle est morte ou false si elle
    /// a survécu.
    /// </summary>
    /// <param name="unitController">L'unité attaqué</param>
    /// <returns>true si l'unité attaqué est morte, false si elle a survécu.</returns>
    public bool Attack(UnitController unitController)
    {
        unitController.HP -= controller.Attack;

        if(unitController.HP <= 0)
        {
            // L'unité attaqué est mort
            Destroy(unitController.gameObject);
            return true;
        }
        else
        {
            // L'unité attaqué a survécu
            return false;
        }
    }
}
