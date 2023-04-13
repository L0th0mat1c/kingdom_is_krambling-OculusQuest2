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
    /// Attaque l'unité en paramètre/
    /// </summary>
    public void Attack(BaseUnitController unitController)
    {
        unitController.ReceiveDamage(controller.Attack);
    }
}
