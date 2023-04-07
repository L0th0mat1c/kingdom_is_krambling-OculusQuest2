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
    /// Attaque l'unit� en param�tre et retourne true si elle est morte ou false si elle
    /// a surv�cu.
    /// </summary>
    /// <param name="unitController">L'unit� attaqu�</param>
    /// <returns>true si l'unit� attaqu� est morte, false si elle a surv�cu.</returns>
    public bool Attack(UnitController unitController)
    {
        unitController.HP -= controller.Attack;

        if(unitController.HP <= 0)
        {
            // L'unit� attaqu� est mort
            Destroy(unitController.gameObject);
            return true;
        }
        else
        {
            // L'unit� attaqu� a surv�cu
            return false;
        }
    }
}
